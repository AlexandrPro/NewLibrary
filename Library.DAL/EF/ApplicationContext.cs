using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Library.Entities;


namespace Library.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("NewLibrary") {
            //ContextOptions.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Magazine> Magazines { get; set; }
        public virtual DbSet<Brochure> Brochures { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }
        public virtual DbSet<BookInPublishingHouse> BookInPublishingHouses { get; set; }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}