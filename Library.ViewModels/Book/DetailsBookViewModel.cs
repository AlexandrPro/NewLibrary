using System;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Book
{
    public class DetailsBookViewModel
    {
        [Required]
        [StringLength(200)]
        public string Author { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime YearOfPublishing { get; set; }
    }
}
