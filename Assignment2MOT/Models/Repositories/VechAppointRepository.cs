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

        // VechAppointRepository Constructory
        public VechAppointRepository()
        {
            this.db = new MOTContext();
        }

        // VechAppointRepository Constructory
        public VechAppointRepository(MOTContext db)
        {
            this.db = db;
        }

        // Delete an appointment
        public void Delete(int id)
        {
            VechAppoint existing = db.VechAppoints.Find(id);
            db.VechAppoints.Remove(existing);
        }

        // Insert an appointment
        public void Insert(VechAppoint obj)
        {
            db.VechAppoints.Add(obj);
        }

        // Save all changes
        public void Save()
        {
            db.SaveChanges();
        }

        // Get all appointments
        public IEnumerable<VechAppoint> SelectAllAppointments()
        {
            return db.VechAppoints.OrderBy(a => a.MOTCentresCentreId).ThenBy(b => b.VechAppointTime).ToList();
        }

        // Get Appointments for Centre matching ID
        public IEnumerable<VechAppoint> SelectCentreAppointments(int id)
        {
            return db.VechAppoints.Where(a => a.MOTCentresCentreId == id).OrderBy(b => b.VechAppointTime).ToList();
        }

        // Get Appointment by ID
        public VechAppoint SelectByID(int id)
        {
            return db.VechAppoints.Find(id);
        }

        // Update an appointment
        public void Update(VechAppoint obj)
        {
            VechAppoint va = SelectByID(obj.VechAppointId);
            va.VechAppointTime = obj.VechAppointTime;
            va.VechOwner = obj.VechOwner;
            va.VechRegNo = obj.VechRegNo;
            va.MOTCentresCentreId = obj.MOTCentresCentreId;
        }

        public IEnumerable<string> SelectAllRegs(int id)
        {
            return db.VechAppoints.Where(m => m.VechAppointId != id).Select(m => m.VechRegNo).ToList();
        }
    }
}