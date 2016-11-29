namespace Assignment2MOT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VechAppoint")]
    public partial class VechAppoint
    {
        [Key]
        public int VechAppointId { get; set; }

        [Required(ErrorMessage = "Vehicle Registration is a required field")]
        [StringLength(7)]
        [Display(Name = "Vehicle Registration:")]
        public string VechRegNo { get; set; }

        [Required(ErrorMessage = "Vehicle Owner is a required field")]
        [Display(Name = "Vehicle Owner:")]
        public string VechOwner { get; set; }

        [Required(ErrorMessage = "Appointment Time is a required field")]
        [Display(Name = "Appointment Time:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime VechAppointTime { get; set; }

        [Required(ErrorMessage = "MOT Centre is a required field")]
        [Display(Name = "MOT Centre:")]
        public int MOTCentresCentreId { get; set; }

        public virtual MOTCentre MOTCentre { get; set; }
    }
}
