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

        public MOTCentreRepository()
        {
            this.db = new MOTContext();
        }

        public MOTCentreRepository(MOTContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            MOTCentre existing = db.MOTCentres.Find(id);
            foreach( var time in GetTimes(existing.CentreId))
            {
                db.CentreTimes.Remove(time);
            }
            db.MOTCentres.Remove(existing);
        }

        public void Insert(MOTCentre obj)
        {
            db.MOTCentres.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<MOTCentre> SelectAll()
        {
            if (db.MOTCentres.Any())
            {
                return db.MOTCentres.OrderBy(a => a.CentreName).Include(c => c.CentreTimes).ToList();
            }
            else
            {
                return Enumerable.Empty<MOTCentre>();
            }
        }

        public MOTCentre SelectByID(int id)
        {
            return db.MOTCentres.Find(id);
        }

        public MOTCentre SelectByName(string name)
        {
            return db.MOTCentres.Where(a => String.Compare(a.CentreName, name) == 0).First();
        }

        public void Update(MOTCentre obj)
        {
            MOTCentre centre = SelectByID(obj.CentreId);
            centre.CentreName = obj.CentreName;
            centre.CentreAddress = obj.CentreAddress;
            centre.CentreCounty = obj.CentreCounty;
            centre.CentreTeleNo = obj.CentreTeleNo;
        }

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

        public IEnumerable<CentreTime> GetTimes(int id)
        {
            return db.CentreTimes.Where(a => a.MOTCentresCentreId == id).ToList();
        }

        public void InsertTime(CentreTime obj)
        {
            db.CentreTimes.Add(obj);
        }
    }
}
