using System.Web.Mvc;
using Library.BLL.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Library.ViewModels.Brochure;

namespace Library.Controllers
{
    public class BrochureController : Controller
    {
        BrochureService brochureSerivice;
        public BrochureController()
        {
            brochureSerivice = new BrochureService();
        }

        // GET: Brochure
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Brochures_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(brochureSerivice.GetAll().brochures.ToDataSourceResult(request));
        }

        // GET: Brochure/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Brochure/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brochure/Create
        [HttpPost]
        public ActionResult Create(CreateBrochureViewModel brochure)
        {
            try
            {
                brochureSerivice.Create(brochure);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brochure/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Brochure/Edit/5
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

        // GET: Brochure/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brochure/Delete/5
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
