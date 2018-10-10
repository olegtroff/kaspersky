using System;
using System.Collections.Generic;
using System.Linq;

namespace InMemoryStorage
{
    public static class StorageUtils
    {
        public static IEnumerable<T> ApplyFilter<T>(IEnumerable<T> data, Func<T, bool> filter)
        {
            return filter == null ? data : data.Where(filter);
        }
    }
}
