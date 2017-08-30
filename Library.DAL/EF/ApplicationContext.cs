using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Library.Entities;


namespace Library.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("NewLibrary") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}