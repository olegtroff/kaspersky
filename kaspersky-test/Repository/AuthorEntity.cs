using System;
using Core;
using Microsoft.WindowsAzure.Storage.Table;

namespace Repository
{
    public class AuthorEntity : TableEntity, IAuthor
    {
        public static AuthorEntity Create(IAuthor author) => new AuthorEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = string.IsNullOrEmpty(author.Id) ? Guid.NewGuid().ToString() : author.Id,
            Name = author.Name,
            LastName = author.LastName
        };

        public string Id => RowKey;
        public string Name { get; set; }
        public string LastName { get; set; }
        public static string GeneratePartitionKey()
        {
            return "author";
        }
    }
}
