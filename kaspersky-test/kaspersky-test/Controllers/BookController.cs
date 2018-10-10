using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Core;
using Core.Repository;
using kaspersky_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace kaspersky_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublishingHouseRepository _publishingRepository;
        public BookController(IBookRepository bookRepository, 
            IAuthorRepository authorRepository,
            IPublishingHouseRepository publishingRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publishingRepository = publishingRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<BookViewModel>> Get([FromQuery] bool sortByYear = false, [FromQuery] bool sortbyDesc = false)
        {
            var books = await _bookRepository.GetList();
            var authors = await _authorRepository.GetList();
            var publishings = await _publishingRepository.GetList();
            var model = books.Select(c => new BookViewModel(c, authors, publishings));
            if (sortByYear)
                model = sortbyDesc ? model.OrderBy(c => c.Year) : model.OrderByDescending(c => c.Year);
            else model = sortbyDesc ? model.OrderBy(c => c.Title) : model.OrderByDescending(c => c.Title);
            return model;
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<EditBookViewModel> Get(string id)
        {
            var model = new EditBookViewModel()
            {
                Authors = await _authorRepository.GetList(),
                Publishings = await _publishingRepository.GetList(),
                Book = await _bookRepository.GetByIdAsync(id)
            };
            return model;
        }

        [HttpPost]
        public async Task Post(IFormCollection data)
        {
            var value = new Book()
            {
                Id = string.IsNullOrEmpty(data["id"]) ? string.Empty : JsonConvert.DeserializeObject<string>(data["id"]),
                Isbn = string.IsNullOrEmpty(data["isbn"]) ? string.Empty : JsonConvert.DeserializeObject<string>(data["isbn"]),
                Title = string.IsNullOrEmpty(data["title"]) ? string.Empty : JsonConvert.DeserializeObject<string>(data["title"]),
                PageCount = string.IsNullOrEmpty(data["pageCount"]) ? 0 : JsonConvert.DeserializeObject<int>(data["pageCount"]),
                Year = string.IsNullOrEmpty(data["year"]) ? 0 : JsonConvert.DeserializeObject<int>(data["year"]),
                Publishing = string.IsNullOrEmpty(data["publishing"]) ? string.Empty : JsonConvert.DeserializeObject<string>(data["publishing"]),
                Authors = string.IsNullOrEmpty(data["publishing"]) ? new List<string>() : JsonConvert.DeserializeObject<IEnumerable<string>>(data["authors"])
            };

            foreach (var file in data.Files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        value.Image = $"data:{file.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
                    }
                }
            }
            await _bookRepository.InsertAsync(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _bookRepository.DeleteById(id);
        }
    }
}
