using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Library.BLL.Services;
using System.Web.Mvc;
using Library.ViewModels.Magazine;

namespace Library.Controllers
{
    public class MagazineController : Controller
    {
        MagazineService magazineService;
        public MagazineController()
        {
            magazineService = new MagazineService();
        }

        // GET: Magazine
        public ActionResult Index()
        {
            //IndexMagazineViewModel magazines = magazineService.GetAll();
            //return View(magazines);
            return View();
        }
        public JsonResult Magazines_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(magazineService.GetAll().magazines.ToDataSourceResult(request));
        }

        // GET: Magazine/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Magazine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Magazine/Create
        [HttpPost]
        public ActionResult Create(CreateMagazineViewModel magazin)
        {
            try
            {
                magazineService.Create(magazin);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Magazine/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Magazine/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Magazine/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Magazine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
