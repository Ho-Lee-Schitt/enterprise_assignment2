using Assignment2MOT.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2MOT.Models;
using System.Text.RegularExpressions;

namespace Assignment2MOT.Controllers
{
    public class VechAppointController : Controller
    {
        private VechAppointRepository AppointRepository = null;
        private MOTCentreRepository MOTRepository = null;

        // Constructor for VechAppointController
        public VechAppointController()
        {
            this.AppointRepository = new VechAppointRepository();
            this.MOTRepository = new MOTCentreRepository();
        }

        // Constructor for VechAppointController for test purposes
        public VechAppointController(VechAppointRepository repository)
        {
            this.AppointRepository = repository;
            this.MOTRepository = new MOTCentreRepository();
        }

        // List the appointments, parameter is optional to display appointments relating to a specified centre
        [HttpGet]
        public ActionResult Index(int id = -1)
        {
            if (id >= 0)
            {
                List<VechAppoint> centreAppoints = (List<VechAppoint>)AppointRepository.SelectCentreAppointments(id);
                return View(centreAppoints);
            }
            List<VechAppoint> model = (List<VechAppoint>)AppointRepository.SelectAllAppointments();
            return View(model);
        }

        // Details an appointment
        [HttpGet]
        public ActionResult Details(int id)
        {
            VechAppoint existing = AppointRepository.SelectByID(id);
            if (existing == null)
            {
                return new HttpNotFoundResult("Invalid Centre ID");
            }
            return View(existing);
        }

        // Creates an appointment
        [HttpGet]
        public ActionResult Create()
        {
            VechAppointViewModel viewModel = new VechAppointViewModel
            {
                MOTCentres = MOTRepository.SelectAll()
            };

            return View(viewModel);
        }

        // Creates an appointment - handles the response back
        [HttpPost]
        public ActionResult Create(VechAppointViewModel obj)
        {
            if (ModelState.IsValid)
            { // check valid state and time
                if (isTimeValid(obj.VechAppointTime, obj.VechAppointDate.Value, obj.MOTCentresCentreId))
                {
                    if (isRegValid(obj.VechRegNo))
                    {
                        VechAppoint newAppoint = convVAVMtoVA(obj);
                        AppointRepository.Insert(newAppoint);
                        AppointRepository.Save();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Reg", "There is already a Vehicle with that Registration Number.");
                        obj.MOTCentres = MOTRepository.SelectAll();
                        return View(obj);
                    }
                }
                else
                {
                    ModelState.AddModelError("Time", "Sorry but the Centre you have selected is not open at this time. Please refer to the MOT Centres Table and reselect a time.");
                    obj.MOTCentres = MOTRepository.SelectAll();
                    return View(obj);
                }
            }
            else // not valid so redisplay
            {
                obj.MOTCentres = MOTRepository.SelectAll();
                return View(obj);
            }
        }

        // Edits an appointment
        [HttpGet, ActionName("Edit")]
        public ActionResult ConfirmEdit(int id)
        {
            VechAppoint existing = AppointRepository.SelectByID(id);
            if (existing == null)
            {
                return new HttpNotFoundResult("Invalid Centre ID");
            }
            ViewData["currentMOTCentre"] = existing.MOTCentre.CentreName;
            VechAppointViewModel vavm = convVAtoVAVM(existing);
            vavm.MOTCentres = MOTRepository.SelectAll();
            return View(vavm);
        }

        // Edits an appointment - handles the response back
        [HttpPost]
        public ActionResult Edit(VechAppointViewModel obj)
        {
            if (ModelState.IsValid)
            { // check valid state and time
                if (isTimeValid(obj.VechAppointTime, obj.VechAppointDate.Value, obj.MOTCentresCentreId))
                {
                    if (isRegValid(obj.VechRegNo, obj.VechAppointId))
                    {
                        VechAppoint updatedAppoint = convVAVMtoVA(obj);
                        AppointRepository.Update(updatedAppoint);
                        AppointRepository.Save();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Reg", "There is already a Vehicle with that Registration Number.");
                        obj.MOTCentres = MOTRepository.SelectAll();
                        return View(obj);
                    }
                }
                else
                {
                    ModelState.AddModelError("Time", "Sorry but the Centre you have selected is not open at this time. Please refer to the MOT Centres Table and reselect a time.");
                    obj.MOTCentres = MOTRepository.SelectAll();
                    return View(obj);
                }
            }
            else // not valid so redisplay
            {
                obj.MOTCentres = MOTRepository.SelectAll();
                return View(obj);
            }
        }

        // Deletes an appointment
        [HttpGet, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            VechAppoint existing = AppointRepository.SelectByID(id);
            if (existing == null)
            {
                return new HttpNotFoundResult("Invalid Centre ID");
            }
            return View(existing);
        }

        // Deletes an appointment - handles the response back
        [HttpPost]
        public ActionResult Delete(int id)
        {
            AppointRepository.Delete(id);
            AppointRepository.Save();
            return RedirectToAction("Index");
        }

        // Converts a VechAppointViewModel to a VechAppoint
        public VechAppoint convVAVMtoVA(VechAppointViewModel vm)
        {
            VechAppoint va = new VechAppoint { MOTCentresCentreId = vm.MOTCentresCentreId, VechAppointId = vm.VechAppointId, VechOwner = vm.VechOwner, VechRegNo = vm.VechRegNo };
            va.VechAppointTime = vm.VechAppointDate.Value.Date + vm.VechAppointTime.TimeOfDay;
            return va;
        }

        // Converts a VechAppoint to a VechAppointViewModel
        public VechAppointViewModel convVAtoVAVM(VechAppoint va)
        {
            VechAppointViewModel vm = new VechAppointViewModel { MOTCentresCentreId = va.MOTCentresCentreId, VechAppointId = va.VechAppointId, VechOwner = va.VechOwner, VechRegNo = Regex.Replace(va.VechRegNo, @"\s+", "") };
            vm.VechAppointTime = DateTime.Parse(va.VechAppointTime.ToString("HH:mm"));
            vm.VechAppointDate = va.VechAppointTime.Date;
            return vm;
        }

        /* Checks that the appointment time is valid
         * i.e. 3pm on the 23/07 - what day is it and does 3pm fall within opening hours
         * 
         * for each time in list
         * if the week day is correct and the time is within the range then all's good
         * otherwise fail
         */
        public bool isTimeValid(DateTime time, DateTime date, int centreId)
        {
            if (MOTRepository.GetTimes(centreId).Any())
            {
                foreach (var ct in MOTRepository.GetTimes(centreId))
                {
                    if (ct.DayOfTheWeek == (int)date.DayOfWeek)
                    {
                        if (time.TimeOfDay > ct.OpeningTime && time.TimeOfDay < ct.ClosingTime)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }

        public bool isRegValid(string regNo, int id = -1)
        {
            if (AppointRepository.SelectAllRegs(id).Any())
            {
                foreach (var reg in AppointRepository.SelectAllRegs(id))
                {
                    if (String.Compare(regNo, Regex.Replace(reg, @"\s+", "")) == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}