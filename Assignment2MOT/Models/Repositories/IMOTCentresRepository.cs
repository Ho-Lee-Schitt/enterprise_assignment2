using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Assignment2MOT.Models.Repositories
{
    public interface IMOTCentresRepository
    {
        IEnumerable<MOTCentre> SelectAll();
        MOTCentre SelectByID(int id);
        void Insert(MOTCentre obj);
        void Update(MOTCentre obj);
        void Delete(int id);
        void Save();
        IEnumerable<CentreTime> GetTimes(int id);
    }
}
