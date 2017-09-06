using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    //public class BaseEntity
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    //    public Guid Id { get; set; }

    //    [Required]
    //    [DataType(DataType.Date)]
    //    public DateTime CreationDate { get; set; }
    //}

    public abstract class BaseEntity
    {
        private String id;
        [Key]
        [Required]
        public String Id
        {
            get
            {
                return id ?? (id = Guid.NewGuid().ToString());
            }
            set
            {
                id = value;
            }
        }

        [Required]
        public DateTime CreationDate
        {
            get;
            set;
        }

        protected BaseEntity()
        {
            DateTime now = DateTime.Now;
            CreationDate = new DateTime(now.Ticks / 100000 * 100000, now.Kind);
        }
    }
}
