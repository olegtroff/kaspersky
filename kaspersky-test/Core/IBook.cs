using System.Collections.Generic;

namespace Core
{
    public interface IBook
    {
        string Id { get; }
        string Title { get; }
        IEnumerable<string> Authors { get; }
        string Publishing { get; }
        int PageCount { get; }
        int Year { get; }
        string Isbn { get; }
        string Image { get; }
    }
}
