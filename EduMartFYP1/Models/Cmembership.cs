using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EduMartFYP1.Models
{
    public class Cmembership
    {
        [Key]
        public int CollegeID { get; set; }
        public string CollegeName { get; set; }
        public string CollegeEmail { get; set; }
        public string CollegePassword { get; set; }
    }
}