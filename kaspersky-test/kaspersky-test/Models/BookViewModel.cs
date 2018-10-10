using System.Collections.Generic;
using System.Linq;
using Core;

namespace kaspersky_test.Models
{
    public class BookViewModel : IBook
    {
        public BookViewModel(IBook book, IEnumerable<IAuthor> authors, IEnumerable<IPublishingHouse> publishings)
        {
            Title = book.Title;
            Id = book.Id;
            PageCount = book.PageCount;
            Year = book.Year;
            Isbn = book.Isbn;
            AuthorNames = GetAuthors(book, authors);
            Publishing = GetPublishing(book, publishings);
            Image = book.Image;
        }

        protected List<string> GetAuthors(IBook book, IEnumerable<IAuthor> authors)
        {
            var list = new List<string>();
            if (book.Authors != null)
            {
                foreach (var author in book.Authors)
                {
                    var authormodel = authors.FirstOrDefault(a => a.Id == author);
                    if (authormodel != null)
                        list.Add(string.Format("{0} {1}", authormodel.Name, authormodel.LastName));
                }
            }
            return list;
        }
        protected string GetPublishing(IBook book, IEnumerable<IPublishingHouse> publishings)
        {
            if (!string.IsNullOrEmpty(book.Publishing))
            {
                return publishings.FirstOrDefault(p => p.Id == book.Publishing)?.Name;
            }
            return string.Empty;
        }
        public List<string> AuthorNames { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Publishing { get; set; }

        public int PageCount { get; set; }

        public int Year { get; set; }

        public string Isbn { get; set; }

        public string Image { get; set; }

        public IEnumerable<string> Authors { get; set; }
    }
}
