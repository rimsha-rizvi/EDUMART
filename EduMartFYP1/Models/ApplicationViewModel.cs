using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduMartFYP1.Models
{
    public class ApplicationViewModel
    {

        public int ApplicationID { get; set; }
        public int StudentID { get; set; }
        public List<CheckBoxViewModel> College { get; set; }
        public List<CheckBoxViewModel> Decipline { get; set; }
        public List<CheckBoxViewModel> Quota { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal ObtainedMarks { get; set; }
        public decimal Percentage { get; set; }
        public System.DateTime Date { get; set; }
    }
}