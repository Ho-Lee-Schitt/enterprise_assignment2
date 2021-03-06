﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment2MOT
{
    public class VechAppointViewModel
    {
        public int VechAppointId { get; set; }

        [Required(ErrorMessage = "Vehicle Registration is a required field")]
        [Display(Name = "Vehicle Registration")]
        [RegularExpression(@"(?<Current>^[A-Z]{2}[0-9]{2}[A-Z]{3}$)|(?<Prefix>^[A-Z][0-9]{1,3}[A-Z]{3}$)|(?<Suffix>^[A-Z]{3}[0-9]{1,3}[A-Z]$)|(?<DatelessLongNumberPrefix>^[0-9]{1,4}[A-Z]{1,2}$)|(?<DatelessShortNumberPrefix>^[0-9]{1,3}[A-Z]{1,3}$)|(?<DatelessLongNumberSuffix>^[A-Z]{1,2}[0-9]{1,4}$)|(?<DatelessShortNumberSufix>^[A-Z]{1,3}[0-9]{1,3}$)", ErrorMessage = "Not a valid Registration number. e.g. ABC123")]
        public string VechRegNo { get; set; }

        [Required(ErrorMessage = "Vehicle Owner is a required field")]
        [Display(Name = "Vehicle Owner")]
        [StringLength(40, ErrorMessage = "Owner's Name can be no larger than 40 characters")]
        public string VechOwner { get; set; }

        [Required(ErrorMessage = "Appointment Date is a required field")]
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        [CustomDateAttribute]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? VechAppointDate { get; set; }

        [Required(ErrorMessage = "Appointment Time is a required field")]
        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        public DateTime VechAppointTime { get; set; }

        [Required(ErrorMessage = "MOT Centre is a required field")]
        [Display(Name = "MOT Centre")]
        public int MOTCentresCentreId { get; set; }

        public IEnumerable<MOTCentre> MOTCentres { get; set; }
    }
}