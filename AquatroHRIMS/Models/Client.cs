using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquatroHRIMS.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Please use alphabets only")]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        [Required(ErrorMessage = "Please enter email id")]
        [System.Web.Mvc.Remote("CheckClientEmail", "Client", ErrorMessage = "Email id already registered")]
        [Display(Name = "Email ID")]
        public string EmailID { get;set;}

        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        [Required(ErrorMessage = "Please enter email id")]
        [System.Web.Mvc.Remote("CheckClientEmailUpdate", "Client", AdditionalFields = "ClientIDHdn", ErrorMessage = "Email id already registered")]
        [Display(Name = "Email ID")]
        public string EmailIDUpdate { get; set; }
        
        

        [Required(ErrorMessage = "Please enter phone no")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Client Phone No.")]
        [System.Web.Mvc.Remote("CheckClientContact", "Client",  ErrorMessage = "Contact no.already registered")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Please enter phone no")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Client Phone No.")]
        [System.Web.Mvc.Remote("CheckClientContactUpdate", "Client", AdditionalFields = "ClientIDHdn", ErrorMessage = "Contact no.already registered")]
        public string ContactUpdate { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        [Display(Name = "Address")]   
        public string Address { get; set; }

        [Display(Name = "Description")] 
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string ClientIDHdn { get; set; }

        public string type { get; set; }
        public int recordID { get; set; }
        public IEnumerable<ClientList> ClientList { get; set; }

    }
    public class ClientList
    {

        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientContact { get; set; }
    }
    public class Externalhead
    {
        [Required(ErrorMessage = "Please enter external head name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter alphabets only")]
        [Display(Name = "External head name")]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        public string Name { get; set; }

        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        [Display(Name = "Email Id")]
        [Required(ErrorMessage = "Please enter external head name")]
        public string Email { get; set; }

        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact No.")]
        [Required(ErrorMessage = "Please enter external head name")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Please select external client ")]
        public SelectList ExternalClientList { get; set; }

        public string[] SelectedExternalClient { get; set; }
    }
    public class ddlExternalClient
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
     
}