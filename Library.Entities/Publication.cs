using Library.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Publication : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public PublicationType Type { get; set; }
    }
}
