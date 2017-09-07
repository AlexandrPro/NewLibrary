using System.Collections.Generic;

namespace Library.Entities
{
    public class BookInPublishingHouse : BaseEntity
    {
        public virtual PublishingHouse PublishingHouse { get; set; }
        public virtual Book Book { get; set; }
    }
}
