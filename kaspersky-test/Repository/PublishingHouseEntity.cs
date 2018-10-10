using System;
using Core;
using Microsoft.WindowsAzure.Storage.Table;

namespace Repository
{
    public class PublishingHouseEntity : TableEntity, IPublishingHouse
    {
        public static PublishingHouseEntity Create(IPublishingHouse house) => new PublishingHouseEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = string.IsNullOrEmpty(house.Id) ? Guid.NewGuid().ToString() : house.Id,
            Name = house.Name
        };

        public string Id => RowKey;
        public string Name { get; set; }
        public static string GeneratePartitionKey()
        {
            return "house";
        }
    }
}
