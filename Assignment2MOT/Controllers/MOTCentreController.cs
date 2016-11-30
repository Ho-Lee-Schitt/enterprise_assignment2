using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment2MOT.Models.Repositories;
using Assignment2MOT.Models;
using System.Text.RegularExpressions;

namespace Assignment2MOT.Controllers
{
    public class MOTCentreController : Controller
    {
        private MOTCentreRepository repository = null;

        public MOTCentreController()
        {
            this.repository = new MOTCentreRepository();
        }

        //public MOTCentreController(MOTCentreRepository repository)
        //{
        //    this.repository = repository;
        //}

        [HttpGet]
        public ActionResult Index()
        {
            List<MOTCentre> model = (List<MOTCentre>)repository.SelectAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            MOTCentre existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="00:00", Value = "0" },
                new SelectListItem{ Text="01:00", Value = "1" },
                new SelectListItem{ Text="02:00", Value = "2" },
                new SelectListItem{ Text="03:00", Value = "3" },
                new SelectListItem{ Text="04:00", Value = "4" },
                new SelectListItem{ Text="05:00", Value = "5" },
                new SelectListItem{ Text="06:00", Value = "6" },
                new SelectListItem{ Text="07:00", Value = "7" },
                new SelectListItem{ Text="08:00", Value = "8" },
                new SelectListItem{ Text="09:00", Value = "9" },
                new SelectListItem{ Text="10:00", Value = "10" },
                new SelectListItem{ Text="11:00", Value = "11" },
                new SelectListItem{ Text="12:00", Value = "12" },
                new SelectListItem{ Text="13:00", Value = "13" },
                new SelectListItem{ Text="14:00", Value = "14" },
                new SelectListItem{ Text="15:00", Value = "15" },
                new SelectListItem{ Text="16:00", Value = "16" },
                new SelectListItem{ Text="17:00", Value = "17" },
                new SelectListItem{ Text="18:00", Value = "18" },
                new SelectListItem{ Text="19:00", Value = "19" },
                new SelectListItem{ Text="20:00", Value = "20" },
                new SelectListItem{ Text="21:00", Value = "21" },
                new SelectListItem{ Text="22:00", Value = "22" },
                new SelectListItem{ Text="23:00", Value = "23" },
            };
            ViewData["TimeList"] = list;
            return View();
        }

        [HttpPost]
        public ActionResult Create(MotCentreTimes obj)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="00:00", Value = "0" },
                new SelectListItem{ Text="01:00", Value = "1" },
                new SelectListItem{ Text="02:00", Value = "2" },
                new SelectListItem{ Text="03:00", Value = "3" },
                new SelectListItem{ Text="04:00", Value = "4" },
                new SelectListItem{ Text="05:00", Value = "5" },
                new SelectListItem{ Text="06:00", Value = "6" },
                new SelectListItem{ Text="07:00", Value = "7" },
                new SelectListItem{ Text="08:00", Value = "8" },
                new SelectListItem{ Text="09:00", Value = "9" },
                new SelectListItem{ Text="10:00", Value = "10" },
                new SelectListItem{ Text="11:00", Value = "11" },
                new SelectListItem{ Text="12:00", Value = "12" },
                new SelectListItem{ Text="13:00", Value = "13" },
                new SelectListItem{ Text="14:00", Value = "14" },
                new SelectListItem{ Text="15:00", Value = "15" },
                new SelectListItem{ Text="16:00", Value = "16" },
                new SelectListItem{ Text="17:00", Value = "17" },
                new SelectListItem{ Text="18:00", Value = "18" },
                new SelectListItem{ Text="19:00", Value = "19" },
                new SelectListItem{ Text="20:00", Value = "20" },
                new SelectListItem{ Text="21:00", Value = "21" },
                new SelectListItem{ Text="22:00", Value = "22" },
                new SelectListItem{ Text="23:00", Value = "23" },
            };
            ViewData["TimeList"] = list;
            if (ModelState.IsValid && checkTimes(obj))
            { // check valid state
                MOTCentre centre = new MOTCentre { CentreName = obj.CentreName, CentreCounty = obj.CentreCounty, CentreTeleNo = long.Parse(Regex.Replace(obj.CentreTeleNo, @"\s+", "")) };
                if (String.IsNullOrEmpty(obj.CentreAddressLn2)) {
                    centre.CentreAddress = obj.CentreAddressLn1 + "," + obj.CentreCounty + "," + obj.CentrePostcode;
                }
                else
                {
                    centre.CentreAddress = obj.CentreAddressLn1 + "," + obj.CentreAddressLn2 + "," + obj.CentreCounty + "," + obj.CentrePostcode;
                }
                repository.Insert(centre);
                repository.Save();
                centre = repository.SelectByName(obj.CentreName);

                for (int i = 0; i < obj.times.Count; i++)
                {
                    if (obj.times[i].OpeningTime.TotalDays != obj.times[i].ClosingTime.TotalDays)
                    {
                        CentreTime time = new CentreTime { DayOfTheWeek = i, OpeningTime = TimeSpan.FromHours(obj.times[i].OpeningTime.TotalDays), ClosingTime = TimeSpan.FromHours(obj.times[i].ClosingTime.TotalDays), MOTCentresCentreId = centre.CentreId };
                        repository.InsertTime(time);
                    }
                }
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
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="00:00", Value = "0" },
                new SelectListItem{ Text="01:00", Value = "1" },
                new SelectListItem{ Text="02:00", Value = "2" },
                new SelectListItem{ Text="03:00", Value = "3" },
                new SelectListItem{ Text="04:00", Value = "4" },
                new SelectListItem{ Text="05:00", Value = "5" },
                new SelectListItem{ Text="06:00", Value = "6" },
                new SelectListItem{ Text="07:00", Value = "7" },
                new SelectListItem{ Text="08:00", Value = "8" },
                new SelectListItem{ Text="09:00", Value = "9" },
                new SelectListItem{ Text="10:00", Value = "10" },
                new SelectListItem{ Text="11:00", Value = "11" },
                new SelectListItem{ Text="12:00", Value = "12" },
                new SelectListItem{ Text="13:00", Value = "13" },
                new SelectListItem{ Text="14:00", Value = "14" },
                new SelectListItem{ Text="15:00", Value = "15" },
                new SelectListItem{ Text="16:00", Value = "16" },
                new SelectListItem{ Text="17:00", Value = "17" },
                new SelectListItem{ Text="18:00", Value = "18" },
                new SelectListItem{ Text="19:00", Value = "19" },
                new SelectListItem{ Text="20:00", Value = "20" },
                new SelectListItem{ Text="21:00", Value = "21" },
                new SelectListItem{ Text="22:00", Value = "22" },
                new SelectListItem{ Text="23:00", Value = "23" },
            };
            ViewData["TimeList"] = list;
            MOTCentre existing = repository.SelectByID(id);
            MotCentreTimes centreTime = convertMCtoMCT(existing);
            return View(centreTime);
        }

        [HttpPost]
        public ActionResult Edit(MotCentreTimes obj)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="00:00", Value = "0" },
                new SelectListItem{ Text="01:00", Value = "1" },
                new SelectListItem{ Text="02:00", Value = "2" },
                new SelectListItem{ Text="03:00", Value = "3" },
                new SelectListItem{ Text="04:00", Value = "4" },
                new SelectListItem{ Text="05:00", Value = "5" },
                new SelectListItem{ Text="06:00", Value = "6" },
                new SelectListItem{ Text="07:00", Value = "7" },
                new SelectListItem{ Text="08:00", Value = "8" },
                new SelectListItem{ Text="09:00", Value = "9" },
                new SelectListItem{ Text="10:00", Value = "10" },
                new SelectListItem{ Text="11:00", Value = "11" },
                new SelectListItem{ Text="12:00", Value = "12" },
                new SelectListItem{ Text="13:00", Value = "13" },
                new SelectListItem{ Text="14:00", Value = "14" },
                new SelectListItem{ Text="15:00", Value = "15" },
                new SelectListItem{ Text="16:00", Value = "16" },
                new SelectListItem{ Text="17:00", Value = "17" },
                new SelectListItem{ Text="18:00", Value = "18" },
                new SelectListItem{ Text="19:00", Value = "19" },
                new SelectListItem{ Text="20:00", Value = "20" },
                new SelectListItem{ Text="21:00", Value = "21" },
                new SelectListItem{ Text="22:00", Value = "22" },
                new SelectListItem{ Text="23:00", Value = "23" },
            };
            ViewData["TimeList"] = list;
            if (ModelState.IsValid && checkTimes(obj))
            { // check valid state
                MOTCentre centre = convertMCTtoMC(obj);
                repository.Update(centre);
                repository.Save();
                centre = repository.SelectByName(obj.CentreName);

                for (int i = 0; i < obj.times.Count; i++)
                {
                        CentreTime time = new CentreTime { DayOfTheWeek = i, OpeningTime = TimeSpan.FromHours(obj.times[i].OpeningTime.TotalDays), ClosingTime = TimeSpan.FromHours(obj.times[i].ClosingTime.TotalDays), MOTCentresCentreId = centre.CentreId };
                        repository.UpdateTime(time);
                }
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
            MOTCentre existing = repository.SelectByID(id);
            return View(existing);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
            return RedirectToAction("Index");
        }

        private bool checkTimes(MotCentreTimes Centre)
        {
            for (int i = 0; i < Centre.times.Count; i++)
            {
                if (TimeSpan.Compare(Centre.times[i].OpeningTime, Centre.times[i].ClosingTime) == 1)
                {
                    return false;
                }
            }
            for (int i = 0; i < Centre.times.Count; i++)
            {
                if (Centre.times[i].OpeningTime != Centre.times[i].ClosingTime)
                {
                    return true;
                }
            }
            return false;
        }

        private MOTCentre convertMCTtoMC(MotCentreTimes centreTime)
        {
            MOTCentre centre = new MOTCentre { CentreId = centreTime.CentreId, CentreName = centreTime.CentreName, CentreCounty = centreTime.CentreCounty, CentreTeleNo = long.Parse(Regex.Replace(centreTime.CentreTeleNo, @"\s+", "")) };
            if (String.IsNullOrEmpty(centreTime.CentreAddressLn2))
            {
                centre.CentreAddress = centreTime.CentreAddressLn1 + "," + centreTime.CentreCounty + "," + centreTime.CentrePostcode;
            }
            else
            {
                centre.CentreAddress = centreTime.CentreAddressLn1 + "," + centreTime.CentreAddressLn2 + "," + centreTime.CentreCounty + "," + centreTime.CentrePostcode;
            }

            return centre;
        }

        private MotCentreTimes convertMCtoMCT(MOTCentre centre)
        {
            MotCentreTimes centreTime = new MotCentreTimes { CentreId = centre.CentreId, CentreName = centre.CentreName, CentreCounty = centre.CentreCounty, CentreTeleNo = ("0" + centre.CentreTeleNo.ToString()) };
            string[] address = centre.CentreAddress.Split(',');
            centreTime.CentreAddressLn1 = address[0];
            if (address.Count() < 4)
            {
                centreTime.CentrePostcode = address[2];
            }
            else
            {
                centreTime.CentreAddressLn2 = address[1];
                centreTime.CentrePostcode = address[3];
            }

            centreTime.times = new List<MotCentreTimes.ct>();
            for (int i = 0; i <= 6; i++)
            {
                centreTime.times.Add(new MotCentreTimes.ct());
            }

            foreach (var time in repository.GetTimes(centreTime.CentreId))
            {
                centreTime.times[(time.DayOfTheWeek)] = new MotCentreTimes.ct { OpeningTime = time.OpeningTime, ClosingTime = time.ClosingTime };
            }

            return centreTime;
        }
    }
}