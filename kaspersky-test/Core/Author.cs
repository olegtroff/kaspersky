using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Author : IAuthor
    {
        public string Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        public static Author Create(IAuthor src)
        {
            return src == null
                ? null
                : new Author
                {
                    Id = src.Id,
                    Name = src.Name,
                    LastName = src.LastName
                };
        }
    }
}
