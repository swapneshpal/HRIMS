using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class EmpEmeragancyContact
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter emergancy contact name")]
        [Display(Name = "Emergancy Contact Name")]
        public string EmergancyContactName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter relationShip  name")]
        [Display(Name = "RelationShip")]
        public string RelationShip { get; set; }

        [Required(ErrorMessage = "Please enter phone no")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }

        public bool IsActive { get; set; }

    }
}