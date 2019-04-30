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
    public class StudentsController : Controller
    {
        private EduMartEntities db = new EduMartEntities();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Student.ToList());
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            var Context = new EduMartEntities();
            if (ModelState.IsValid)
            {
                var username = (from s in Context.Student
                                where s.Email == student.Email && s.Password == student.Password
                                select s.FirstName).FirstOrDefault();
                if (username == null)
                {
                    db.Student.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Application/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email Already Exists");
                    return View();
                }
            }

            return View(student);
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
