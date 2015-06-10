using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class EmpEducationDetails
    {

        public int iID { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter institute ")]
        [Display(Name="Institute")]
        public string InstituteName { get; set; }

        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter qualification")]
        [Display(Name = "Qualification")]
        public string qualification { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter univerSity")]
        [Display(Name = "UniverSity")]
        public string UniverSity { get; set; }

        [Required(ErrorMessage = "Please enter degree type ")]
        [Display(Name = "Degree Type")]
        public string DegreeType { get; set; }

        [Required(ErrorMessage = "Please enter passing year ")]
        [StringLength(4, ErrorMessage = "Please enter 4 digit in year")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please use numeric only")]
        [Display(Name = "Passing Year")]
        public string PassingYear { get; set; }

        [RegularExpression(@"^(100\.00|100\.0|100)|([0-9]{1,2}){0,1}(\.[0-9]{1,2}){0,1}$", ErrorMessage = "Please enter valid percentage ")]
        [Required(ErrorMessage = "Please enter percentage ")]
        [Display(Name = "Percentage")]
        public double Percentage { get; set; }

        [Required(ErrorMessage = "Please enter specialization ")]
        [Display(Name = "Specialization")]
        public string  Specialization { get; set; }

        [Required(ErrorMessage = "Please enter certification ")]
        [Display(Name = "Certification")]
        public string Certification { get; set; }



        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please use alphabets only ")]
        [Required(ErrorMessage = "Please enter institute")]
        [Display(Name = "Institute")]
        public string SecInstituteName { get; set; }

        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter qualification")]
        [Display(Name = "Qualification")]
        public string Secqualification { get; set; }

        [StringLength(4, ErrorMessage = "Please enter 4 digit in year")]
        [Display(Name = "Passing Year")]
        [Required(ErrorMessage = "Please enter passing year")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please use numeric only")]
        public string SecPassingYear { get; set; }


        [Required(ErrorMessage = "Please enter percentage")]
        [RegularExpression(@"^(100\.00|100\.0|100)|([0-9]{1,2}){0,1}(\.[0-9]{1,2}){0,1}$", ErrorMessage = "Please enter valid percentage ")]
        [Display(Name = "Percentage")]
        public double SecPercentage { get; set; }


        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter institute")]
        [Display(Name = "Institute")]
        public string HighlySecInstituteName { get; set; }

        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Please use alphabets only")]
        [Display(Name = "Qualification")]
        [Required(ErrorMessage = "Please enter qualification")]
        public string HighlySecqualification { get; set; }

        [Display(Name = "Passing Year")]
        [StringLength(4, ErrorMessage = "Please enter 4 digit in year")]
        [Required(ErrorMessage = "Please enter passing year")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please use numeric only")]
        public string HighlySecPassingYear { get; set; }

        [RegularExpression(@"^(100\.00|100\.0|100)|([0-9]{1,2}){0,1}(\.[0-9]{1,2}){0,1}$", ErrorMessage = "Please enter valid percentage ")]
        [Required(ErrorMessage = "Please enter percentage")]
        [Display(Name = "Percentage")]
        public double HighlySecPercentage { get; set; }

        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "Please enter specialization")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use alphabets only")]
        public string HighlySecSpecialization { get; set; }

        public bool IsActive { get; set; }
    }
}