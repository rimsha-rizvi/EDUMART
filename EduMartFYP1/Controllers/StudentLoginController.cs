using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMartFYP1.Models;
using System.Web.Security;

namespace EduMartFYP1.Controllers
{
    public class StudentLoginController : Controller
    {
        // GET: StudentLogin
        public ActionResult Login()
        {
            if (Session["id"] == null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("", "Home/Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Membership model)
        {
            using (var Context = new EduMartEntities())
            {
                bool isvalid = Context.Student.Any(x => x.Email == model.Email && x.Password == model.Password);
                if (isvalid)
                {
                    var username = (from s in Context.Student
                                    where s.Email == model.Email && s.Password == model.Password
                                    select s.FirstName).FirstOrDefault() ;
                    var id = (from s in Context.Student
                                    where s.Email == model.Email && s.Password == model.Password
                                    select s.StudentID).FirstOrDefault();
                    Session["username"] = username;
                    Session["id"] = id;
                    ViewBag.username = username;
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Index", "Applications/Index");
                }
                ModelState.AddModelError("", "Invalid Username and Password");
                return View();


            }
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}