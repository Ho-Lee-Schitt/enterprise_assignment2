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
        }
    }
}