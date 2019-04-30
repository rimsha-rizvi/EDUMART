using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EduMartFYP1.Controllers
{
    public class CollegeLoginController : Controller
    {
        // GET: CollegeLogin
        public ActionResult Login()
        {
            if (Session["username"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Applications/Index");
            }
        }

        [HttpPost]
        public ActionResult Login(EduMartFYP1.Models.Cmembership model)
        {
            using (var Context = new EduMartEntities())
            {

                    bool isvalid = Context.College.Any(x => x.CollegeEmail == model.CollegeEmail && x.CollegePassword == model.CollegePassword);
                    if (isvalid)
                    {
                        var username = (from c in Context.College
                                        where c.CollegeEmail == model.CollegeEmail && c.CollegePassword == model.CollegePassword
                                        select c.CollegeName).FirstOrDefault();
                        var id = (from c in Context.College
                                  where c.CollegeEmail == model.CollegeEmail && c.CollegePassword == model.CollegePassword
                                  select c.CollegeID).FirstOrDefault();
                        Session["Collegeusername"] = username;
                        Session["Collegeid"] = id;
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