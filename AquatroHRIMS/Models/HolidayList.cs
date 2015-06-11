using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HRMS;
namespace AquatroHRIMS.Models
{
    public class HolidayList
    {


        [Required(ErrorMessage = "Please enter occasion name")]
        [Display(Name = "Occasion")]
        public string Occassion { get; set; }

        [Required(ErrorMessage = "Please select holiday date")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Please enter holiday description")]
        public string Description { get; set; }


        public string HolidayName { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public string allDay { get; set; }


   
    }
    public class Event
    {
        public string title { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public string url { get; set; }
        public string backgroundColor { get; set; }

       
    }
  

}