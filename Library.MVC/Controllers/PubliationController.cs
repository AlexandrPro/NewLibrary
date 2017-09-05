using Library.BLL.Services;
using Library.ViewModels.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.MVC.Controllers
{
    public class PubliationController : Controller
    {
        PublicationService publicationService;
        public PubliationController()
        {
            publicationService = new PublicationService();
        }
        // GET: Publiation
        public ActionResult Index()
        {
            IndexPublicationViewModel publication = publicationService.GetAll();
            return View(publication);
        }

        // GET: Publiation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Publiation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publiation/Create
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

        // GET: Publiation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Publiation/Edit/5
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

        // GET: Publiation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Publiation/Delete/5
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
