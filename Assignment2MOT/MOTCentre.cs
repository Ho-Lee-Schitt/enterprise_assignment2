using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Assignment2MOT
{
    public partial class MOTCentre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MOTCentre()
        {
            CentreTimes = new HashSet<CentreTime>();
            VechAppoints = new HashSet<VechAppoint>();
        }

        [Key]
        public int CentreId { get; set; }

        [Required]
        public string CentreName { get; set; }

        [Required]
        public string CentreAddress { get; set; }

        [DisplayFormat(DataFormatString = "{0:0## #### ####}")]
        public long CentreTeleNo { get; set; }

        [Required]
        public string CentreCounty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CentreTime> CentreTimes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VechAppoint> VechAppoints { get; set; }
    }
}