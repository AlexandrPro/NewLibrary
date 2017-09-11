using System.Web.Mvc;
using Library.BLL.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Library.ViewModels.Brochure;

namespace Library.Controllers
{
    [Authorize]
    public class BrochureController : Controller
    {
        BrochureService brochureSerivice;
        public BrochureController()
        {
            brochureSerivice = new BrochureService();
        }

        // GET: Brochure
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult Brochures_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(brochureSerivice.GetAll().brochures/*.ToDataSourceResult(request)*/, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Admin()
        {
            IndexBrochureViewModel brochures = brochureSerivice.GetAll();
            return View(brochures);
        }

        // GET: Brochure/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            DetailsBrochureViewModel detailsViewModel = brochureSerivice.GetByIdDetails(id);
            return View(detailsViewModel);
        }

        // GET: Brochure/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brochure/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateBrochureViewModel brochure)
        {
            try
            {
                brochureSerivice.Create(brochure);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brochure/Edit/5
        public ActionResult Edit(string id)
        {
            EditBrochureViewModel brochureViewModel = brochureSerivice.GetByIdEdit(id);
            return View(brochureViewModel);
        }

        // POST: Brochure/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, EditBrochureViewModel brochureViewModel)
        {
            try
            {
                if (brochureViewModel != null)
                    brochureSerivice.Edit(id, brochureViewModel);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brochure/Delete/5
        public ActionResult Delete(string id)
        {
            DeleteBrochureViewModel brochureViewModel = brochureSerivice.GetByIdDelete(id);
            return View(brochureViewModel);
        }

        // POST: Brochure/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                if (brochureSerivice.GetByIdDelete(id) != null)
                    brochureSerivice.Delete(id);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }
    }
}
