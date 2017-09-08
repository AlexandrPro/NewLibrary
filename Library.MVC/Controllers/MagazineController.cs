using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Library.BLL.Services;
using System.Web.Mvc;
using Library.ViewModels.Magazine;

namespace Library.Controllers
{
    [Authorize]
    public class MagazineController : Controller
    {
        MagazineService magazineService;
        public MagazineController()
        {
            magazineService = new MagazineService();
        }

        // GET: Magazine
        [AllowAnonymous]
        public ActionResult Index()
        {
            IndexMagazineViewModel magazines = magazineService.GetAll();
            return View(magazines);
        }

        [AllowAnonymous]
        public JsonResult Magazines_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(magazineService.GetAll().magazines.ToDataSourceResult(request));
        }
        public ActionResult Admin()
        {
            IndexMagazineViewModel magazines = magazineService.GetAll();
            return View(magazines);
        }

        // GET: Magazine/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Magazine/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Magazine/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateMagazineViewModel magazin)
        {
            try
            {
                magazineService.Create(magazin);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Magazine/Edit/5
        public ActionResult Edit(string id)
        {
            EditMagazineViewModel magazineViewModel = magazineService.GetByIdEdit(id);
            return View(magazineViewModel);
        }

        // POST: Magazine/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, EditMagazineViewModel magazineViewModel)
        {
            try
            {
                if (magazineViewModel != null)
                    magazineService.Edit(id, magazineViewModel);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Magazine/Delete/5
        public ActionResult Delete(string id)
        {
            DeleteMagazineViewModel magazineViewModel = magazineService.GetByIdDelete(id);
            return View(magazineViewModel);
        }

        // POST: Magazine/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                if (magazineService.GetByIdDelete(id) != null)
                    magazineService.Delete(id);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }
    }
}
