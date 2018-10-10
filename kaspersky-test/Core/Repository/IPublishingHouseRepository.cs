using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IPublishingHouseRepository
    {
        Task<PublishingHouse> GetByIdAsync(string id);
        Task InsertAsync(IPublishingHouse house);
        Task<IEnumerable<IPublishingHouse>> GetList();
        Task DeleteById(string id);
    }
}
