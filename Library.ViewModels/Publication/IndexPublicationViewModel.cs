using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.ViewModels.Publication
{
    public class IndexPublicationViewModel
    {
        public List<PublicationViewModel> publications { get; set; }
    }

    public class PublicationViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
