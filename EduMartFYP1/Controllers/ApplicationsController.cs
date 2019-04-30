using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduMartFYP1;
using EduMartFYP1.Models;

namespace EduMartFYP1.Controllers
{
    public class ApplicationsController : Controller
    {
        private EduMartEntities db = new EduMartEntities();

        // GET: Applications
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                var application = db.Application.Include(a => a.College).Include(a => a.Decipline).Include(a => a.Quota).Include(a => a.Student);
                return View(application.ToList());
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }
        // GET: CollegeApplications
        public ActionResult CollegeApplications()
        {
            int id = Convert.ToInt32(Session["Collegeid"]);
            if (Session["Collegeid"] == null)
            {
                return RedirectToAction("", "CollegeLogin/Login");
            }
            else
            {
                //var application = db.Application.Include(a => id).Include(a => a.Decipline).Include(a => a.Quota).Include(a => a.Student);
                return View(db.Application.Where(a => a.CollegeID == id).ToList());
            }
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["id"]);
            var userid = (from s in db.Student
                            where s.StudentID == id
                            select s.FirstName).FirstOrDefault();
            Session["existingid"] = id;
            if (Session["existingid"] != null)
            {
                return RedirectToAction("Edit", "Edit");
            }
            if (Session["id"] == null)
            {
               return RedirectToAction("","StudentLogin/Login");
            }
            else
            {
                var Results = from c in db.College
                              select new
                              {
                                  c.CollegeID,
                                  c.CollegeName,
                                  c.CollegeAddress,
                                  c.CollegeContact
                              };
                var Myviewmodel = new ApplicationViewModel();

                var MyCheckBoxListCollege = new List<CheckBoxViewModel>();
                var MyCheckBoxListDecipline = new List<CheckBoxViewModel>();
                var MyCheckBoxListQuota = new List<CheckBoxViewModel>();

                foreach (var item in Results)
                {
                    MyCheckBoxListCollege.Add(new CheckBoxViewModel { Id = item.CollegeID, Name = item.CollegeName, Checked = false });
                }

                var Results1 = from d in db.Decipline
                               select new
                               {
                                   d.DeciplineID,
                                   d.DeciplineName
                               };

                foreach (var item1 in Results1)
                {
                    MyCheckBoxListDecipline.Add(new CheckBoxViewModel { Id = item1.DeciplineID, Name = item1.DeciplineName, Checked = false });
                }

                var Results2 = from q in db.Quota
                               select new
                               {
                                   q.QuotaID,
                                   q.QuotaName
                               };

                foreach (var item2 in Results2)
                {
                    MyCheckBoxListQuota.Add(new CheckBoxViewModel { Id = item2.QuotaID, Name = item2.QuotaName, Checked = false });
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

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationViewModel application)
        {
            int id = Convert.ToInt32(Session["id"]);
            if (ModelState.IsValid)
            {
                foreach (var item in application.College)
                {
                    if (item.Checked)
                    {
                        foreach (var item1 in application.Decipline)
                        {
                            if (item1.Checked)
                            {
                                foreach (var item2 in application.Quota)
                                {
                                    if (item2.Checked)
                                    {
                                        db.Application.Add(new Application() { StudentID = id, CollegeID = item.Id, DeciplineID = item1.Id, QuotaID = item2.Id, Date = application.Date, ObtainedMarks = application.ObtainedMarks, TotalMarks = application.TotalMarks, Percentage = application.Percentage });
                                    }
                                }
                            }
                        }
                    }
                }
                //db.Application.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CollegeID = new SelectList(db.College, "CollegeID", "CollegeName", application.CollegeID);
            //ViewBag.DeciplineID = new SelectList(db.Decipline, "DeciplineID", "DeciplineName", application.DeciplineID);
            //ViewBag.QuotaID = new SelectList(db.Quotas, "QuotaID", "QuotaName", application.QuotaID);
            //ViewBag.StudentID = new SelectList(db.Student, "StudentID", "FirstName", application.StudentID);
            return View(application);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
