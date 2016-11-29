using Assignment2MOT.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2MOT.Models;
using AutoMapper;

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
            List<VechAppoint> model = (List<VechAppoint>)repository.SelectAllAppointments();
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
            VechAppointViewModel viewModel = new VechAppointViewModel
            {
                MOTCentres = repository.SelectAllCentres()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(VechAppointViewModel obj)
        {
            if (ModelState.IsValid)
            { // check valid state
                VechAppoint newAppoint = convVAVMtoVA(obj);
                repository.Insert(newAppoint);
                repository.Save();
                return RedirectToAction("Index");
            }
            else // not valid so redisplay
            {
                obj.MOTCentres = repository.SelectAllCentres();
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

        public VechAppoint convVAVMtoVA(VechAppointViewModel vm)
        {
            VechAppoint va = new VechAppoint { MOTCentresCentreId = vm.MOTCentresCentreId, VechAppointId = vm.VechAppointId, VechOwner = vm.VechOwner, VechRegNo = vm.VechRegNo, VechAppointTime = vm.VechAppointTime };
            return va;
        }

        public VechAppoint convVAtoVAVM(VechAppoint va)
        {
            VechAppoint vm = new VechAppoint { MOTCentresCentreId = va.MOTCentresCentreId, VechAppointId = va.VechAppointId, VechOwner = va.VechOwner, VechRegNo = va.VechRegNo, VechAppointTime = va.VechAppointTime };
            return vm;
        }
    }
}