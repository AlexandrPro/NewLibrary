using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library.Entities
{
    [Table("Book")]
    public partial class Book : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Author { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }


        //[DataType(DataType.Date)]
        public DateTime YearOfPublishing { get; set; }

        [Required]
        public string PublicationId { get; set; }
    }
}
