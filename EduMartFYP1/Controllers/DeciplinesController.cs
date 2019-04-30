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
    public class DeciplinesController : Controller
    {
        private EduMartEntities db = new EduMartEntities();

        // GET: Deciplines
        public ActionResult Index()
        {
            var adminname = Convert.ToString(Session["admin"]);
            var admin = "admin";
            if (adminname == admin)
            {
                return View(db.Decipline.ToList());
            }
            else
            {
                if (Session["admin"] != null)
                {
                    return RedirectToAction("", "Home");
                }
                else
                {
                    return RedirectToAction("", "Admins/Login");
                }
            }
        }

        // GET: Deciplines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decipline decipline = db.Decipline.Find(id);
            if (decipline == null)
            {
                return HttpNotFound();
            }
            var adminname = Convert.ToString(Session["admin"]);
            var admin = "admin";
            if (adminname == admin)
            {
                return View(decipline);
            }
            else
            {
                if (Session["admin"] != null)
                {
                    return RedirectToAction("", "Home");
                }
                else
                {
                    return RedirectToAction("", "Admins/Login");
                }
            }
        }

        // GET: Deciplines/Create
        public ActionResult Create()
        {
            if (Session["admin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // POST: Deciplines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeciplineID,DeciplineName")] Decipline decipline)
        {
            if (ModelState.IsValid)
            {
                db.Decipline.Add(decipline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(decipline);
        }

        // GET: Deciplines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decipline decipline = db.Decipline.Find(id);
            if (decipline == null)
            {
                return HttpNotFound();
            }
            if (Session["admin"] != null)
            {
                return View(decipline);
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // POST: Deciplines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeciplineID,DeciplineName")] Decipline decipline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(decipline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(decipline);
        }

        // GET: Deciplines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decipline decipline = db.Decipline.Find(id);
            if (decipline == null)
            {
                return HttpNotFound();
            } 
            if (Session["admin"] != null)
            {
                return View(decipline);
            }
            else
            {
                return RedirectToAction("", "Home");
            }

        }

        // POST: Deciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Decipline decipline = db.Decipline.Find(id);
            db.Decipline.Remove(decipline);
            db.SaveChanges();
            return RedirectToAction("Index");
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
