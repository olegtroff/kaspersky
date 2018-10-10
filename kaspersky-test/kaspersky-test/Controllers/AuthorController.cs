using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace kaspersky_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<IAuthor>> Get()
        {
            return await _authorRepository.GetList();
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<Author> Get(string id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Author value)
        {
            await _authorRepository.InsertAsync(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _authorRepository.DeleteById(id);
        }
    }
}
