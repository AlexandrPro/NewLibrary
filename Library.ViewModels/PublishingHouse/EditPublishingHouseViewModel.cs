using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.PublishingHouse
{
    public class EditPublishingHouseViewModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }
    }
}
