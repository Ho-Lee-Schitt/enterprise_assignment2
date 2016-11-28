using Assignment2MOT.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2MOT.Models;

namespace Assignment2MOT.Controllers
{
    public class VechAppointController : Controller
    {
        private VechAppointRepository repository = null;

        public VechAppointController()
        {
            this.repository = new VechAppointRepository();
        }

        //public VechAppointController(VechAppointRepository repository)
        //{
        //    this.repository = repository;
        //}

        [HttpGet]
        public ActionResult Index()
        {
            List<VechAppoint> model = (List<VechAppoint>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            VechAppoint existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VechAppoint obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Insert(obj);
                repository.Save();
                return RedirectToAction("Index");
            }
            else // not valid so redisplay
            {
                return View(obj);
            }
        }

        [HttpGet, ActionName("Edit")]
        public ActionResult ConfirmEdit(int id)
        {
            VechAppoint existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpPost]
        public ActionResult Edit(VechAppoint obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                repository.Update(obj);
                repository.Save();
                return RedirectToAction("Index");
            }
            else // not valid so redisplay
            {
                return View(obj);
            }
        }

        [HttpGet, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            VechAppoint existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
            return RedirectToAction("Index");
        }
    }
}