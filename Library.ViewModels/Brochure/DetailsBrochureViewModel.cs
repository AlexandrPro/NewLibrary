using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Brochure
{
    public class DetailsBrochureViewModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string TypeOfCover { get; set; }

        public int NumberOfPages { get; set; }
    }
}
