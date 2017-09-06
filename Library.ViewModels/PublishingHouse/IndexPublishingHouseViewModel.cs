using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.ViewModels.PublishingHouse
{
    public class IndexPublishingHouseViewModel
    {
        public List<PublishingHouseViewModel> publishingHouses { get; set; }
    }

    public class PublishingHouseViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }
    }
}
