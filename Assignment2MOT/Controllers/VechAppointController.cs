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
            if (ModelState.IsValid && isTimeValid(obj.VechAppointTime, obj.VechAppointDate, obj.MOTCentresCentreId))
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
            ViewData["currentMOTCentre"] = existing.MOTCentre.CentreName;
            VechAppointViewModel vavm = convVAtoVAVM(existing);
            vavm.MOTCentres = repository.SelectAllCentres();
            return View(vavm);
        }

        [HttpPost]
        public ActionResult Edit(VechAppointViewModel obj)
        {
            if (ModelState.IsValid && isTimeValid(obj.VechAppointTime, obj.VechAppointDate, obj.MOTCentresCentreId))
            { // check valid state
                VechAppoint updatedAppoint = convVAVMtoVA(obj);
                repository.Update(updatedAppoint);
                repository.Save();
                return RedirectToAction("Index");
            }
            else // not valid so redisplay
            {
                obj.MOTCentres = repository.SelectAllCentres();
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
            VechAppoint va = new VechAppoint { MOTCentresCentreId = vm.MOTCentresCentreId, VechAppointId = vm.VechAppointId, VechOwner = vm.VechOwner, VechRegNo = vm.VechRegNo };
            va.VechAppointTime = vm.VechAppointDate.Date + vm.VechAppointTime.TimeOfDay;
            return va;
        }

        public VechAppointViewModel convVAtoVAVM(VechAppoint va)
        {
            VechAppointViewModel vm = new VechAppointViewModel { MOTCentresCentreId = va.MOTCentresCentreId, VechAppointId = va.VechAppointId, VechOwner = va.VechOwner, VechRegNo = va.VechRegNo };
            vm.VechAppointTime = DateTime.Parse(va.VechAppointTime.ToString("HH:mm"));
            vm.VechAppointDate = va.VechAppointTime.Date;
            return vm;
        }

        public bool isTimeValid(DateTime time, DateTime date, int centreId)
        {
            if (repository.GetCentreTimes(centreId).Any())
            {
                foreach (var ct in repository.GetCentreTimes(centreId))
                {
                    if(ct.DayOfTheWeek == (int)date.DayOfWeek) {
                        if (time.TimeOfDay < ct.OpeningTime || time.TimeOfDay > ct.ClosingTime)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}