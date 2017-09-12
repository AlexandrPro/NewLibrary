using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.PublishingHouse
{
    public class CreatePublishingHouseViewModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        public List<string> BookIds { get; set; }
    }
}
