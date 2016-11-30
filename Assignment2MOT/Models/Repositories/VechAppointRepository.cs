using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assignment2MOT.Models.Repositories
{
    public class VechAppointRepository : IVechAppointRepository
    {
        private MOTContext db = null;

        public VechAppointRepository()
        {
            this.db = new MOTContext();
        }

        public VechAppointRepository(MOTContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            VechAppoint existing = db.VechAppoints.Find(id);
            db.VechAppoints.Remove(existing);
        }

        public void Insert(VechAppoint obj)
        {
            db.VechAppoints.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<VechAppoint> SelectAllAppointments()
        {
            return db.VechAppoints.OrderBy(a => a.MOTCentresCentreId).ThenBy(b => b.VechAppointTime).ToList();
        }

        public IEnumerable<VechAppoint> SelectCentreAppointments(int id)
        {
            return db.VechAppoints.Where(a => a.MOTCentresCentreId == id).OrderBy(b => b.VechAppointTime).ToList();
        }

        public IEnumerable<MOTCentre> SelectAllCentres()
        {
            return db.MOTCentres.OrderBy(a => a.CentreId).ToList();
        }

        public VechAppoint SelectByID(int id)
        {
            return db.VechAppoints.Find(id);
        }

        public void Update(VechAppoint obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            VechAppoint va = SelectByID(obj.VechAppointId);
            va.VechAppointTime = obj.VechAppointTime;
            va.VechOwner = obj.VechOwner;
            va.VechRegNo = obj.VechRegNo;
            va.MOTCentresCentreId = obj.MOTCentresCentreId;
        }

        public IEnumerable<CentreTime> GetCentreTimes (int id)
        {
            return db.CentreTimes.Where(t => t.MOTCentresCentreId == id).ToList();
        }
    }
}