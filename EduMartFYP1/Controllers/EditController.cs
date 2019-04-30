using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMartFYP1.Models;

namespace EduMartFYP1.Controllers
{
    public class EditController : Controller
    {
        private EduMartEntities db = new EduMartEntities();
        // GET: Edit
        public ActionResult Edit()
        {
            int id = Convert.ToInt32(Session["id"]);

            if (Session["id"] == null)
            {
                return RedirectToAction("", "StudentLogin/Login");
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            var Results = from c in db.College
                          select new
                          {
                              c.CollegeID,
                              c.CollegeName,
                              c.CollegeAddress,
                              c.CollegeContact,
                              Checked = ((from ap in db.Application
                                          where (ap.StudentID == id) & (ap.CollegeID == c.CollegeID)
                                          select ap
                                              ).Count() > 0)
                          };
            var Myviewmodel = new ApplicationViewModel();

            var MyCheckBoxListCollege = new List<CheckBoxViewModel>();
            var MyCheckBoxListDecipline = new List<CheckBoxViewModel>();
            var MyCheckBoxListQuota = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxListCollege.Add(new CheckBoxViewModel { Id = item.CollegeID, Name = item.CollegeName, Checked = item.Checked });
            }

            var Results1 = from d in db.Decipline
                           select new
                           {
                               d.DeciplineID,
                               d.DeciplineName,
                               Checked = ((from ap in db.Application
                                           where (ap.StudentID == id) & (ap.DeciplineID == d.DeciplineID)
                                           select ap
                                              ).Count() > 0)
                           };

            foreach (var item1 in Results1)
            {
                MyCheckBoxListDecipline.Add(new CheckBoxViewModel { Id = item1.DeciplineID, Name = item1.DeciplineName, Checked = item1.Checked });
            }

            var Results2 = from q in db.Quota
                           select new
                           {
                               q.QuotaID,
                               q.QuotaName,
                               Checked = ((from ap in db.Application
                                           where (ap.StudentID == id) & (ap.QuotaID == q.QuotaID)
                                           select ap
                                              ).Count() > 0)
                           };

            foreach (var item2 in Results2)
            {
                MyCheckBoxListQuota.Add(new CheckBoxViewModel { Id = item2.QuotaID, Name = item2.QuotaName, Checked = item2.Checked });
            }
            Myviewmodel.College = MyCheckBoxListCollege;
            Myviewmodel.Decipline = MyCheckBoxListDecipline;
            Myviewmodel.Quota = MyCheckBoxListQuota;
            //ViewBag.CollegeID = new SelectList(db.College, "CollegeID", "CollegeName");
            //
            //ViewBag.DeciplineID = new SelectList(db.Decipline, "DeciplineID", "DeciplineName");
            //ViewBag.QuotaID = new SelectList(db.Quotas, "QuotaID", "QuotaName");
            //ViewBag.StudentID = new SelectList(db.Student, "StudentID", "FirstName");
            //var user = UserManagerExtensions.FindById(User.Identity.GetUserId()) ;
            return View(Myviewmodel);
        }
    }
}