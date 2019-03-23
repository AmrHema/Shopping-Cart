using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Chart_Leader.CustomHelpersValidation;
namespace Chart_Leader.Models
{
    [MetadataType(typeof(Customers_Custom))]
    public partial class Customers
    {
       

    }
    public class Customers_Custom
    {
        public int CID { get; set; }
        [Required]
        [StringLength(20,MinimumLength =3)]
        public string FName { get; set; }

        [Required]
        //[StringLength(7, MinimumLength = 7, ErrorMessage = "This field must be 7 characters")]
        [StringLength(20, MinimumLength = 3)]
        public string LName { get; set; }

        [Required]
        [RegularExpression(@"(.{11})", ErrorMessage = "This field must be 11 characters")]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]

        public string Postcode { get; set; }
        public string Ctype { get; set; }
        [Required]
        public string CardNo { get; set; }

        [CurrentDate(ErrorMessage = "Credit card has already expired")]
        public System.DateTime ExpDate { get; set; }
        public string Email { get; set; }
    }
}