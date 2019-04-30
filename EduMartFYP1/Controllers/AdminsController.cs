using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduMartFYP1;
using System.Web.Security;

namespace EduMartFYP1.Controllers
{
    public class AdminsController : Controller
    {
        private EduMartEntities db = new EduMartEntities();

        // GET: Admins
        public ActionResult Index()
        {
            var adminname = Convert.ToString(Session["admin"]);
            var admin = "admin";
            if (adminname == admin)
            {
                return View(db.Admin.ToList());
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            var adminname = Convert.ToString(Session["admin"]);
            var admins = "admin";
            if (adminname == admins)
            {
                return View(admin);
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            var adminname = Convert.ToString(Session["admin"]);
            var admins = "admin";
            if (adminname == admins)
            {
                return View();
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adminid,adminusername,adminpassword")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            var adminname = Convert.ToString(Session["admin"]);
            var admins = "admin";
            if (adminname == admins)
            {
                return View(admin);
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminid,adminusername,adminpassword")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            var adminname = Convert.ToString(Session["admin"]);
            var admins = "admin";
            if (adminname == admins)
            {
                return View(admin);
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admin.Find(id);
            db.Admin.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            if (Session["admin"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("", "Home");
            }
            
        }
        [HttpPost]
        public ActionResult Login(EduMartFYP1.Models.Adminmembership model)
        {
            using (var Context = new EduMartEntities())
            {
                bool isvalid = Context.Admin.Any(x => x.adminusername == model.adminusername && x.adminpassword == model.adminpassword);
                if (isvalid)
                {
                    var username = (from a in Context.Admin
                                    where a.adminusername == model.adminusername && a.adminpassword == model.adminpassword
                                    select a.adminusername).FirstOrDefault();
                    var id = (from a in Context.Admin
                              where a.adminusername == model.adminusername && a.adminpassword == model.adminpassword
                              select a.adminid).FirstOrDefault();
                    Session["admin"] = username;
                    Session["adminid"] = id;
                    ViewBag.username = username;
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Index", "Applications/Index");
                }
                ModelState.AddModelError("", "Invalid Username and Password");
                return View();


            }
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
