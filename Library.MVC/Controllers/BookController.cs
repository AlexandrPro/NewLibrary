﻿using System.Web.Mvc;
using Library.Services;
using Kendo.Mvc.UI;
using Library.ViewModels.Book;

namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        BookService bookService;
        public BookController()
        {
            bookService = new BookService();
        }

        // GET: Book
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult Books_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(bookService.GetAll().books/*.ToDataSourceResult(request)*/, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PublishingHouses_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(bookService.GetAllPublishingHouses().publishingHouses/*.ToDataSourceResult(request)*/, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Admin()
        {
            IndexBookViewModel books = bookService.GetAll();
            return View(books);
        }

        // GET: Book/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            DetailsBookViewModel bookViewModel = bookService.GetByIdDetails(id);
            return View(bookViewModel);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateBookViewModel bookViewModel)
        {
            try
            {
                bookService.Create(bookViewModel);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            EditBookViewModel bookViewModel = bookService.GetByIdEdit(id);
            return View(bookViewModel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, EditBookViewModel bookViewModel)
        {
            try
            {
                if(bookViewModel != null)
                    bookService.Edit(id, bookViewModel);
                
                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult SelectedPublishingHouses_Read(string id, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(bookService.GetBookPublishubgHouses(id)/*.ToDataSourceResult(request)*/, JsonRequestBehavior.AllowGet);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(string id)
        {
            DeleteBookViewModel bookViewModel = bookService.GetByIdDelete(id);
            return View(bookViewModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                if(bookService.GetByIdDelete(id) != null)
                    bookService.Delete(id);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View();
            }
        }
    }
}