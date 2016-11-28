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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VechAppointId { get; set; }

        [Required]
        [StringLength(7)]
        public string VechRegNo { get; set; }

        [Required]
        public string VechOwner { get; set; }

        public DateTime VechAppointTime { get; set; }

        public int MOTCentresCentreId { get; set; }

        public virtual MOTCentre MOTCentre { get; set; }
    }
}
