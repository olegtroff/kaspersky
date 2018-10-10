using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace kaspersky_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishingHouseController : ControllerBase
    {
        private readonly IPublishingHouseRepository _publishingHouseRepository;
        public PublishingHouseController(IPublishingHouseRepository publishingHouseRepository)
        {
            _publishingHouseRepository = publishingHouseRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<IPublishingHouse>> Get()
        {
            return await _publishingHouseRepository.GetList();
        }

        [HttpGet("{id}", Name = "GetHouse")]
        public async Task<PublishingHouse> Get(string id)
        {
            return await _publishingHouseRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task Post([FromBody] PublishingHouse value)
        {
            await _publishingHouseRepository.InsertAsync(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _publishingHouseRepository.DeleteById(id);
        }
    }
}
