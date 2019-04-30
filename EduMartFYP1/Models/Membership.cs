using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduMartFYP1.Models
{
    public class Membership
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}