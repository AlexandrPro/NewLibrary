using System;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Magazine
{
    public class DeleteMagazineViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }

        [DataType(DataType.Date)]
        public DateTime YearOfPublishing { get; set; }
    }
}
