using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Repository;
using InMemoryStorage;

namespace Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IInMemoryStorage<AuthorEntity> _authorTablestorage;

        public AuthorRepository(IInMemoryStorage<AuthorEntity> authorTablestorage)
        {
            _authorTablestorage = authorTablestorage;
            InitData();
        }

        protected async void InitData()
        {
            await InsertAsync(new Author() { Name = "Антон", LastName = "Чехов" });
            await InsertAsync(new Author() { Name = "Сергей", LastName = "Есенин" });
        }

        public async Task<Author> GetByIdAsync(string id)
        {
            var src = await _authorTablestorage.GetDataAsync(AuthorEntity.GeneratePartitionKey(), id);
            return Author.Create(src);
        }

        public async Task InsertAsync(IAuthor author)
        {
            await _authorTablestorage.InsertOrMergeAsync(AuthorEntity.Create(author));
        }

        public async Task<IEnumerable<IAuthor>> GetList()
        {
            return await _authorTablestorage.GetDataAsync(AuthorEntity.GeneratePartitionKey());
        }

        public async Task DeleteById(string id)
        {
            await _authorTablestorage.DeleteAsync(AuthorEntity.GeneratePartitionKey(), id);
        }
    }
}
