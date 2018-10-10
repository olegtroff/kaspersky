using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace InMemoryStorage
{
    public class InMemoryStorage<T> : IInMemoryStorage<T> where T : class, ITableEntity, new()
    {
        public string Name => "InMemory";

        private readonly SemaphoreSlim _lockSlim = new SemaphoreSlim(1);

        private readonly ConcurrentDictionary<string, Partition> _partitions = new ConcurrentDictionary<string, Partition>();

        private IEnumerable<T> GetAllData(Func<T, bool> filter = null)
        {
            var result = from partition in _partitions.Values from row in partition.Rows.Values select row.Deserialize<T>();
            if (filter != null)
                result = result.Where(filter);

            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetAllData().GetEnumerator();
        }

        private Partition GetPartition(string partitionKey, bool createNewIfNotExists)
        {
            if (!createNewIfNotExists)
            {
                if (_partitions.TryGetValue(partitionKey, out var data))
                    return data;
                return null;
            }

            return _partitions.GetOrAdd(partitionKey, new Partition());
        }

        private Row GetRow(string partitionKey, string rowKey)
        {
            var partition = GetPartition(partitionKey, false);

            if (partition == null)
                return null;

            return partition.Rows.TryGetValue(rowKey, out var data) ? data : null;
        }

        private void PrivateInsert(T item)
        {
            var partition = GetPartition(item.PartitionKey, true);
            partition.Rows.TryAdd(item.RowKey, Row.Serialize(item));
        }

        public async Task InsertOrMergeAsync(T item)
        {
            await _lockSlim.WaitAsync();
            try
            {
                var row = GetRow(item.PartitionKey, item.RowKey);

                if (row == null)
                    PrivateInsert(item);
                else
                    row.Merge(item);
            }
            finally
            {
                _lockSlim.Release();
            }
        }

        public async Task<T> DeleteAsync(string partitionKey, string rowKey)
        {
            await _lockSlim.WaitAsync();
            try
            {
                var row = GetRow(partitionKey, rowKey);
                if (row == null)
                    return null;

                var partition = GetPartition(partitionKey, false);

                if (!partition.Rows.ContainsKey(rowKey))
                    return null;

                var itemToDelete = partition.Rows[rowKey];
                partition.Rows.TryRemove(rowKey, out var _);
                return itemToDelete.Deserialize<T>();
            }
            finally
            {
                _lockSlim.Release();
            }
        }

        public T this[string partitionKey, string rowKey]
        {
            get
            {
                var row = GetRow(partitionKey, rowKey);

                return row?.Deserialize<T>();
            }
        }

        private readonly T[] _empty = new T[0];

        public Task<T> GetDataAsync(string partition, string row)
        {
            return Task.FromResult(this[partition, row]);
        }

        public IEnumerable<T> this[string partitionKey]
        {
            get
            {
                var partition = GetPartition(partitionKey, false);
                return partition?.Rows.Values.Select(row => row.Deserialize<T>()).ToArray() ?? _empty;
            }
        }

        public Task<IEnumerable<T>> GetDataAsync(string partition, Func<T, bool> filter = null)
        {
            return Task.Run(() => StorageUtils.ApplyFilter(this[partition], filter));
        }
    }
}