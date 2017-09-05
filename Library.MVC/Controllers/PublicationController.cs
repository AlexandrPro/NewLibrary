using Library.BLL.Services;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;


namespace Library.Controllers
{
    public class PublicationController : Controller
    {
        PublicationService publicationService;
        public PublicationController()
        {
            publicationService = new PublicationService();
        }
        // GET: Item
        public ActionResult Index()
        {
            //IndexItemViewModel items = itemService.GetAll();
            //return View(items);
            return View();
        }

        public JsonResult Publications_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(publicationService.GetAll().publications.ToDataSourceResult(request));
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Item/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Item/Edit/5
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

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Item/Delete/5
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