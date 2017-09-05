using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Publication : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
