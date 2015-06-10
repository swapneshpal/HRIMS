using System;
using HRIMS;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AquatroHRIMS.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.ViewModel
{

    public class ProjectViewModel

    {
            public Project objProject { get; set; }

            [Required(ErrorMessage="Please enter project description")]
            public string ProjectDescription { get; set; }

            public ExternalProjectHead objExternalProject { get; set; }

            public SelectList Internalheadlist { get; set; }
            public string[] SelectedInternalHead { get; set; }

            public SelectList ClientList { get; set; }
            public string[] SelectedClient { get; set; }

            public SelectList ExternalHeadList { get; set; }
            public string[] SelectedExternalHead { get; set; }

            public SelectList StatusList { get; set; }
            public string[] SelectedStatus{ get; set; }

            public SelectList ProjectList { get; set; }

            public string[] SelectedProject { get; set; }

            public SelectList EmpList { get; set; }

            public string[] SelectedEmpList{ get; set; }

            public string  hdnProjectID { get; set; }

   }


    public class ddlInternalHead
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }

    public class ddlClient
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }

    public class ddlExternalHead
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }


    public class ddlstatus
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }
    public class ddlProject
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }
    public class ddlEmpList
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
}

