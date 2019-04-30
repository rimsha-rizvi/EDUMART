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
    public class QuotasController : Controller
    {
        private EduMartEntities db = new EduMartEntities();

        // GET: Quotas
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                return View(db.Quota.ToList());
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // GET: Quotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quota quota = db.Quota.Find(id);
            if (quota == null)
            {
                return HttpNotFound();
            }
            if (Session["admin"] != null)
            {
                return View(quota);
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // GET: Quotas/Create
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

        // POST: Quotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuotaID,QuotaName")] Quota quota)
        {
            if (ModelState.IsValid)
            {
                db.Quota.Add(quota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quota);
        }

        // GET: Quotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quota quota = db.Quota.Find(id);
            if (quota == null)
            {
                return HttpNotFound();
            }
            if (Session["admin"] != null)
            {
                return View(quota);
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // POST: Quotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuotaID,QuotaName")] Quota quota)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Quotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quota quota = db.Quota.Find(id);
            if (quota == null)
            {
                return HttpNotFound();
            }
            if (Session["admin"] != null)
            {
                return View(quota);
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        // POST: Quotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quota quota = db.Quota.Find(id);
            db.Quota.Remove(quota);
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
