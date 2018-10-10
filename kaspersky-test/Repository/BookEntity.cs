using System;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using Core;

namespace Repository
{
    public class BookEntity : TableEntity, IBook
    {
        public static BookEntity Create(IBook book) => new BookEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = string.IsNullOrEmpty(book.Id) ? Guid.NewGuid().ToString() : book.Id,
            Authors = book.Authors,
            Isbn = book.Isbn,
            PageCount = book.PageCount,
            Title = book.Title,
            Year = book.Year,
            Publishing = book.Publishing,
            Image = book.Image
        };

        public string Id => RowKey;

        public string Title { get; set; }

        public string Publishing { get; set; }

        public IEnumerable<string> Authors { get; set; }

        public int PageCount { get; set; }

        public int Year { get; set; }

        public string Isbn { get; set; }

        public string Image { get; set; }

        public static string GeneratePartitionKey()
        {
            return "book";
        }
    }
}
