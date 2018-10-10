using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace InMemoryStorage
{
    public interface IInMemoryStorage<T> : IEnumerable<T> where T : ITableEntity, new()
    {
        string Name { get; }

        T this[string partition, string row] { get; }

        IEnumerable<T> this[string partition] { get; }

        Task InsertOrMergeAsync(T item);

        Task<T> DeleteAsync(string partitionKey, string rowKey);

        Task<T> GetDataAsync(string partition, string row);

        Task<IEnumerable<T>> GetDataAsync(string partition, Func<T, bool> filter = null);
    }
}
