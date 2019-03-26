using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectCode.Models;

namespace ProjectCode.Areas.admin.Controllers
{
    public class CountryMastersController : Controller
    {
        private dbOPIEntities db = new dbOPIEntities();

        // GET: admin/CountryMasters
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            return View(db.CountryMasters.ToList());
        }

        // GET: admin/CountryMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryMaster countryMaster = db.CountryMasters.Find(id);
            if (countryMaster == null)
            {
                return HttpNotFound();
            }
            return View(countryMaster);
        }

        // GET: admin/CountryMasters/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            return View();
        }

        // POST: admin/CountryMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryId,CountryName")] CountryMaster countryMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.CountryMasters.Add(countryMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(countryMaster);
        }

        // GET: admin/CountryMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryMaster countryMaster = db.CountryMasters.Find(id);
            if (countryMaster == null)
            {
                return HttpNotFound();
            }
            return View(countryMaster);
        }

        // POST: admin/CountryMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryId,CountryName")] CountryMaster countryMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(countryMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(countryMaster);
        }

        // GET: admin/CountryMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryMaster countryMaster = db.CountryMasters.Find(id);
            if (countryMaster == null)
            {
                return HttpNotFound();
            }
            return View(countryMaster);
        }

        // POST: admin/CountryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            CountryMaster countryMaster = db.CountryMasters.Find(id);
            db.CountryMasters.Remove(countryMaster);
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
