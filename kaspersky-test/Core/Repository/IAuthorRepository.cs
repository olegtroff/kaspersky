using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(string id);
        Task InsertAsync(IAuthor house);
        Task<IEnumerable<IAuthor>> GetList();
        Task DeleteById(string id);
    }
}
