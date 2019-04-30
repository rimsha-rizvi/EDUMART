using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EduMartFYP1.Models
{
    public class Adminmembership
    {
        [Key]
        public int adminid { get; set; }
        public string adminusername { get; set; }
        public string adminpassword { get; set; }
    }
}