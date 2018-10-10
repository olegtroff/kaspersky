using System.Collections.Concurrent;

namespace InMemoryStorage
{
    public class Partition
    {
        public Partition()
        {
            Rows = new ConcurrentDictionary<string, Row>();
        }

        public ConcurrentDictionary<string, Row> Rows { get; private set; }
    }
}
