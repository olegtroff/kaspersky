using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Repository;
using InMemoryStorage;

namespace Repository
{
    public class PublishingHouseRepository : IPublishingHouseRepository
    {
        private readonly IInMemoryStorage<PublishingHouseEntity> _publishingHouseTablestorage;
        public PublishingHouseRepository(IInMemoryStorage<PublishingHouseEntity> publishingHouseTablestorage)
        {
            _publishingHouseTablestorage = publishingHouseTablestorage;
            InitData();
        }

        protected async void InitData()
        {
            await InsertAsync(new PublishingHouse() { Name = "Дом книги" });
            await InsertAsync(new PublishingHouse() { Name = "Издательский дом Москвы" });
        }

        public async Task<PublishingHouse> GetByIdAsync(string id)
        {
            var src = await _publishingHouseTablestorage.GetDataAsync(PublishingHouseEntity.GeneratePartitionKey(), id);
            return PublishingHouse.Create(src);
        }

        public async Task InsertAsync(IPublishingHouse house)
        {
            await _publishingHouseTablestorage.InsertOrMergeAsync(PublishingHouseEntity.Create(house));
        }

        public async Task<IEnumerable<IPublishingHouse>> GetList()
        {
            return await _publishingHouseTablestorage.GetDataAsync(PublishingHouseEntity.GeneratePartitionKey());
        }

        public async Task DeleteById(string id)
        {
            await _publishingHouseTablestorage.DeleteAsync(PublishingHouseEntity.GeneratePartitionKey(), id);
        }
    }
}
