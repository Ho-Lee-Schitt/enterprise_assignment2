using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Assignment2MOT.Models.Repositories
{
    public interface IVechAppointRepository
    {
        IEnumerable<VechAppoint> SelectAllAppointments();
        VechAppoint SelectByID(int id);
        void Insert(VechAppoint obj);
        void Update(VechAppoint obj);
        void Delete(int id);
        void Save();
    }
}
