using System.Collections.Generic;
using Core;

namespace kaspersky_test.Models
{
    public class EditBookViewModel
    {
        public IEnumerable<IPublishingHouse> Publishings { get; set; }
        public IEnumerable<IAuthor> Authors { get; set; }
        public Book Book { get; set; }
    }
}
