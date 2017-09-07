using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class BookInPublishingHouse : BaseEntity
    {
        [Required]
        public virtual PublishingHouse PublishingHouse { get; set; }

        [Required]
        public virtual Book Book { get; set; }
    }
}
