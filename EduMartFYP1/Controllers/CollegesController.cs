using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduMartFYP1;

namespace EduMartFYP1.Controllers
{
    public class CollegesController : Controller
    {
        private EduMartEntities db = new EduMartEntities();

        // GET: Colleges
        public ActionResult Index()
        {
            return View(db.College.ToList());
        }
        // GET: Colleges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colleges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(College college)
        {
            using (var Context = new EduMartEntities())
            if (ModelState.IsValid)
            {
                var username = (from c in Context.College
                                where c.CollegeEmail == college.CollegeEmail && c.CollegePassword == college.CollegePassword
                                select c.CollegeName).FirstOrDefault();
                if (username == null)
                {
                    db.College.Add(college);
                    db.SaveChanges();
                    return RedirectToAction("Application/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email Already Exists");
                    return View();
                }
            }

            return View(college);
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
