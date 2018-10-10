using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(string id);
        Task InsertAsync(IBook house);
        Task<IEnumerable<IBook>> GetList();
        Task DeleteById(string id);
    }
}
