using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2MOT.Models.Repositories
{
    class MOTCentreRepository : IMOTCentresRepository
    {
        private MOTContext db = null;

        // MOTCentreRepository Constructor
        public MOTCentreRepository()
        {
            this.db = new MOTContext();
        }

        // MOTCentreRepository Constructor 
        public MOTCentreRepository(MOTContext db)
        {
            this.db = db;
        }
        
        // Delete a MOT Centre, delete's all times associated with it too
        public void Delete(int id)
        {
            MOTCentre existing = db.MOTCentres.Find(id);
            foreach( var time in GetTimes(existing.CentreId))
            {
                db.CentreTimes.Remove(time);
            }
            db.MOTCentres.Remove(existing);
        }

        // Insert a MOT Centre
        public void Insert(MOTCentre obj)
        {
            db.MOTCentres.Add(obj);
        }

        // Save changes
        public void Save()
        {
            db.SaveChanges();
        }

        // Select all MOT Centres
        public IEnumerable<MOTCentre> SelectAll()
        {
            return db.MOTCentres.OrderBy(a => a.CentreName).Include(c => c.CentreTimes).ToList();
        }

        // Select MOT Centre by id
        public MOTCentre SelectByID(int id)
        {
            return db.MOTCentres.Find(id);
        }

        // Select MOT Centre by name
        public MOTCentre SelectByName(string name)
        {
            return db.MOTCentres.Where(a => String.Compare(a.CentreName, name) == 0).First();
        }

        // Update a MOT Centre
        public void Update(MOTCentre obj)
        {
            MOTCentre centre = SelectByID(obj.CentreId);
            centre.CentreName = obj.CentreName;
            centre.CentreAddress = obj.CentreAddress;
            centre.CentreCounty = obj.CentreCounty;
            centre.CentreTeleNo = obj.CentreTeleNo;
        }

        // Update a MOT Centre's opening hours
        public void UpdateTime(CentreTime obj)
        {
            IEnumerable<CentreTime> time = db.CentreTimes.Where(t => t.MOTCentresCentreId == obj.MOTCentresCentreId && t.DayOfTheWeek == obj.DayOfTheWeek);
            //if time == 00 delete record if exists
            if (obj.OpeningTime == obj.ClosingTime)
            {
                if (time.Any())
                {
                    db.CentreTimes.Remove(time.First());
                }
            }
            // Else we add the time if it doesn't exist or update it if it does
            else
            {
                if (!time.Any())
                {
                    db.CentreTimes.Add(obj);
                }
                else
                {
                    time.First().OpeningTime = obj.OpeningTime;
                    time.First().ClosingTime = obj.ClosingTime;
                }
            }
        }

        // Get opening hours of a MOT Centre
        public IEnumerable<CentreTime> GetTimes(int id)
        {
            return db.CentreTimes.Where(a => a.MOTCentresCentreId == id).ToList();
        }

        // Insert a Centre Time
        public void InsertTime(CentreTime obj)
        {
            db.CentreTimes.Add(obj);
        }
    }
}
