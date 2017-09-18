using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{

    public class Magazine : BaseEntity
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime YearOfPublishing { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
