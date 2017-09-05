using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Library.Entities;


namespace Library.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("NewLibrary") { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        
        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}