using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Library.MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Library.ViewModels.ApplicationUser;
using Library.Entities;

namespace Library.MVC.Controllers
{
    public class ApplicationUserController : Controller
    {
        private ApplicationUserService UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(ApplicationUserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "ApplicationUser");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(ApplicationUserLoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Publication");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "ApplicationUser");
                }
            }
            return RedirectToAction("Index", "Publication");
        }

        [Authorize]
        public async Task<ActionResult> Edit()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                ApplicationUserEditViewModel model = new ApplicationUserEditViewModel { Email = user.Email };
                return View(model);
            }
            return RedirectToAction("Login", "ApplicationUser");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(ApplicationUserEditViewModel model)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.UserName = model.Email;
                //change password
                UserManager.RemovePassword(user.Id);
                UserManager.AddPassword(user.Id, model.Password);
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Publication");
                }
                else
                {
                    ModelState.AddModelError("", "Something wrong");
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Current()
        {
            ApplicationUserCurrentViewModel model = new ApplicationUserCurrentViewModel { Name = User.Identity.Name };
            return View(model);
        }

    }
}