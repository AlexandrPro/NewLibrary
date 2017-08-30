using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Library.MVC.Models
{
    public class ApplicationUserService : UserManager<ApplicationUser>
    {
        public ApplicationUserService(IUserStore<ApplicationUser> store) 
            : base(store) 
    {
        }
        public static ApplicationUserService Create(IdentityFactoryOptions<ApplicationUserService> options,
                                                IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserService manager = new ApplicationUserService(new UserStore<ApplicationUser>(db));
            return manager;
        }
    }
}