using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Book : IBook
    {
        public static Book Create(IBook src)
        {
            return src == null
                ? null
                : new Book
                {
                    Id = src.Id,
                    Year = src.Year,
                    Isbn = src.Isbn,
                    Authors = src.Authors,
                    Title = src.Title,
                    PageCount = src.PageCount,
                    Publishing = src.Publishing,
                    Image = src.Image
                };
        }
        public string Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        public IEnumerable<string> Authors { get; set; }
        public string Publishing { get; set; }
        [Required]
        [MinLength(0)]
        [MaxLength(10000)]
        public int PageCount { get; set; }
        [Required]
        [MinLength(1800)]
        public int Year { get; set; }

        public string Isbn { get; set; }
        public string Image { get; set; }
    }
}
