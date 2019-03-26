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
    public class StateMastersController : Controller
    {
        private dbOPIEntities db = new dbOPIEntities();

        // GET: admin/StateMasters
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            var stateMasters = db.StateMasters.Include(s => s.CountryMaster);
            return View(stateMasters.ToList());
        }

        // GET: admin/StateMasters/Details/5
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
            StateMaster stateMaster = db.StateMasters.Find(id);
            if (stateMaster == null)
            {
                return HttpNotFound();
            }
            return View(stateMaster);
        }

        // GET: admin/StateMasters/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            ViewBag.refCountryId = new SelectList(db.CountryMasters, "CountryId", "CountryName");
            return View();
        }

        // POST: admin/StateMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StateId,StateName,refCountryId")] StateMaster stateMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.StateMasters.Add(stateMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.refCountryId = new SelectList(db.CountryMasters, "CountryId", "CountryName", stateMaster.refCountryId);
            return View(stateMaster);
        }

        // GET: admin/StateMasters/Edit/5
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
            StateMaster stateMaster = db.StateMasters.Find(id);
            if (stateMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.refCountryId = new SelectList(db.CountryMasters, "CountryId", "CountryName", stateMaster.refCountryId);
            return View(stateMaster);
        }

        // POST: admin/StateMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StateId,StateName,refCountryId")] StateMaster stateMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(stateMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.refCountryId = new SelectList(db.CountryMasters, "CountryId", "CountryName", stateMaster.refCountryId);
            return View(stateMaster);
        }

        // GET: admin/StateMasters/Delete/5
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
            StateMaster stateMaster = db.StateMasters.Find(id);
            if (stateMaster == null)
            {
                return HttpNotFound();
            }
            return View(stateMaster);
        }

        // POST: admin/StateMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            StateMaster stateMaster = db.StateMasters.Find(id);
            db.StateMasters.Remove(stateMaster);
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
