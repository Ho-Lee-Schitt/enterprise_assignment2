using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2MOT
{
    public class MotCentreTimes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MotCentreTimes()
        {
            CentreTimes = new HashSet<CentreTime>();
            VechAppoints = new HashSet<VechAppoint>();
        }

        public int CentreId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Centre Name:")]
        [StringLength(40, ErrorMessage = "Centre names can be no larger than 40 characters")]
        public string CentreName { get; set; }

        [Required(ErrorMessage = "Address is a required field")]
        [Display(Name = "Address Line 1:")]
        [StringLength(40, ErrorMessage = "Address can be no larger than 40 characters")]
        public string CentreAddressLn1 { get; set; }

        [Display(Name = "Address Line 2:")]
        [StringLength(40, ErrorMessage = "Address can be no larger than 40 characters")]
        public string CentreAddressLn2 { get; set; }

        [Required(ErrorMessage = "Your must provide a Phone Number")]
        [Display(Name = "Contact Number:")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number. e.g. 028 3752 2234")]
        [DataType(DataType.PhoneNumber)]
        public string CentreTeleNo { get; set; }

        [Required(ErrorMessage = "County is a required field")]
        [Display(Name = "County:")]
        [StringLength(20, ErrorMessage = "County can be no larger than 20 characters")]
        public string CentreCounty { get; set; }

        [Required(ErrorMessage = "Postcode is a required field")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode:")]
        [RegularExpression("^(BT([1-9]|1[0-9]|2[0-9]|3[0-9]|4[0-9]|5[0-9]|6[0-9]|7[0-9]|8[0-9]|9[0-4]) ?[0-9][A-Z]{2})$", ErrorMessage = "Not a valid Postcode. e.g. BT48 0AB")]
        public string CentrePostcode { get; set; }

        public struct ct
        {
            public TimeSpan OpeningTime { get; set; }
            public TimeSpan ClosingTime { get; set; }
        }

        [Display(Name = "Opening Hours:")]
        public List<ct> times { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CentreTime> CentreTimes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VechAppoint> VechAppoints { get; set; }
    }
}