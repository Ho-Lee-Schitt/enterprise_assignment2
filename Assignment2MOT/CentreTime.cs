namespace Assignment2MOT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CentreTime
    {
        public int CentreTimeId { get; set; }

        public int MOTCentresCentreId { get; set; }

        public int DayOfTheWeek { get; set; }

        public TimeSpan OpeningTime { get; set; }
 
        public TimeSpan ClosingTime { get; set; }

        public virtual MOTCentre MOTCentre { get; set; }
    }
}
