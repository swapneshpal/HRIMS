using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AquatroHRIMS.Models
{
    public class EmployeePersonalDetails
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "Employee Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]     
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        public string Gender { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter father name")]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter mother name")]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }


        [Required(ErrorMessage = "Please enter current address")]
        [Display(Name = "Current Address")]
        public string CurrentAddress { get; set; }


        [Required(ErrorMessage = "Please enter permanent address")]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter martial status")]
        [Display(Name = "Martial Status")]
        public string MartialStatus { get; set; }

        //[Required(ErrorMessage = "Please select date of birth")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Time)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please enter pan card/social security no.")]
        [Display(Name = "Government id")]
        public string GovID { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter city")]
        [Display(Name = "City")]
        public string City { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        
        [Required(ErrorMessage = "Please enter state")]
        [Display(Name = "State")]
        public string State { get; set; }

   
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please use number only")]
        [Required(ErrorMessage = "Please enter zip code")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter nationality")]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Please enter phone no")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Personal Contact No.")]
        public string PersonalContactNo { get; set; }

        [Required(ErrorMessage = "Please enter phone no")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Work Contact No.")]
        public string WorkContact { get; set; }

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

        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Please enter email id")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        [Display(Name = "Personal Email Id")]
        public string PersonalEmail { get; set; }


        [Required(ErrorMessage = "Please enter email name")]
        [Display(Name = "Work Email Id")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        public string WorkEmail { get; set; }


        public bool IsActive { get; set; }
    }
}