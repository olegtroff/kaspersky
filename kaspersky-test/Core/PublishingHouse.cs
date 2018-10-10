using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class PublishingHouse : IPublishingHouse
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public string Id { get; set; }
        public static PublishingHouse Create(IPublishingHouse src)
        {
            return src == null
                ? null
                : new PublishingHouse
                {
                    Id = src.Id,
                    Name = src.Name
                };
        }
    }
}
