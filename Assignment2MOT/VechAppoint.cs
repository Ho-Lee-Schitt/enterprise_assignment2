using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Assignment2MOT
{
    [Table("VechAppoint")]
    public partial class VechAppoint
    {
        /*
         * This class is never used to take data for the user and so therefore no data validation is applied to the fields.
         */

        [Key]
        public int VechAppointId { get; set; }

        [Required(ErrorMessage = "Vehicle Registration is a required field")]
        [StringLength(7)]
        [Display(Name = "Vehicle Registration")]
        public string VechRegNo { get; set; }

        [Required(ErrorMessage = "Vehicle Owner is a required field")]
        [Display(Name = "Vehicle Owner")]
        public string VechOwner { get; set; }

        [Required(ErrorMessage = "Appointment Time is a required field")]
        [Display(Name = "Appointment Time")]
        [DisplayFormat(DataFormatString = "{0:ddd dd MMM yyyy a\\t HH:mm}")]
        public DateTime VechAppointTime { get; set; }

        [Required(ErrorMessage = "MOT Centre is a required field")]
        [Display(Name = "MOT Centre")]
        public int MOTCentresCentreId { get; set; }

        public virtual MOTCentre MOTCentre { get; set; }
    }
}
