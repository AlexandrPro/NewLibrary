using System.Web.Mvc;
using Library.BLL.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Library.ViewModels.PublishingHouse;
using System;

namespace Library.Controllers
{
    [Authorize]
    public class PublishingHouseController : Controller
    {
        PublishingHouseService publishingHouseService;
        public PublishingHouseController()
        {
            publishingHouseService = new PublishingHouseService();
        }

        // GET: PublishingHouse
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult PublishingHouses_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(publishingHouseService.GetAll().publishingHouses/*.ToDataSourceResult(request)*/, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Books_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(publishingHouseService.GetAllBooks().Books/*.ToDataSourceResult(request)*/, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Admin()
        {
            IndexPublishingHouseViewModel publishingHouses = publishingHouseService.GetAll();
            return View(publishingHouses);
        }

        // GET: PublishingHouse/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: PublishingHouse/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublishingHouse/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreatePublishingHouseViewModel publishingHouseViewModel)
        {
            try
            {
                publishingHouseService.Create(publishingHouseViewModel);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: PublishingHouse/Edit/5
        [Authorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            EditPublishingHouseViewModel publishingHouseViewModel = publishingHouseService.GetByIdEdit(id);
            return View(publishingHouseViewModel);
        }

        // POST: PublishingHouse/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(string id, EditPublishingHouseViewModel publishingHouseViewModel)
        {
            try
            {
                if (publishingHouseViewModel != null)
                    publishingHouseService.Edit(id, publishingHouseViewModel);

                return RedirectToAction("Admin");
            }
            catch
            {
                throw;
                //return View();
            }
        }


        public JsonResult SelectedBooks_Read(string id, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(publishingHouseService.GetPublishubgHouseBooks(id)/*.ToDataSourceResult(request)*/, JsonRequestBehavior.AllowGet);
        }

        // GET: PublishingHouse/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            DeletePublishingHouseViewModel publishingHouseViewModel = publishingHouseService.GetByIdDelete(id);
            return View(publishingHouseViewModel);
        }

        // POST: PublishingHouse/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                if (publishingHouseService.GetByIdDelete(id) != null)
                    publishingHouseService.Delete(id);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }
    }
}