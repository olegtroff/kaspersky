using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Repository;
using InMemoryStorage;

namespace Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IInMemoryStorage<BookEntity> _bookTablestorage;
        public BookRepository(IInMemoryStorage<BookEntity> bookTablestorage)
        {
            _bookTablestorage = bookTablestorage;
            InitData();
        }

        protected async void InitData()
        {
            await InsertAsync(new Book() { Year = 2018, Isbn = "978-1-4493-4485-6", PageCount = 300, Title = "Отверженные" });
            await InsertAsync(new Book() { Year = 2017, Isbn = "978-1-4493-4486-6", PageCount = 600, Title = "Война и мир" });
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            var src = await _bookTablestorage.GetDataAsync(BookEntity.GeneratePartitionKey(), id);
            return Book.Create(src);
        }

        public async Task InsertAsync(IBook book)
        {
            await _bookTablestorage.InsertOrMergeAsync(BookEntity.Create(book));
        }

        public async Task<IEnumerable<IBook>> GetList()
        {
            return await _bookTablestorage.GetDataAsync(BookEntity.GeneratePartitionKey());
        }
        public async Task DeleteById(string id)
        {
            await _bookTablestorage.DeleteAsync(BookEntity.GeneratePartitionKey(), id);
        }
    }
}
