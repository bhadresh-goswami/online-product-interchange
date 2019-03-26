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
    public class CityMastersController : Controller
    {
        private dbOPIEntities db = new dbOPIEntities();

        // GET: admin/CityMasters
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            var cityMasters = db.CityMasters.Include(c => c.StateMaster);
            return View(cityMasters.ToList());
        }

        // GET: admin/CityMasters/Details/5
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
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            return View(cityMaster);
        }

        // GET: admin/CityMasters/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            ViewBag.refStateId = new SelectList(db.StateMasters, "StateId", "StateName");
            return View();
        }

        // POST: admin/CityMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityId,CityName,refStateId")] CityMaster cityMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.CityMasters.Add(cityMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.refStateId = new SelectList(db.StateMasters, "StateId", "StateName", cityMaster.refStateId);
            return View(cityMaster);
        }

        // GET: admin/CityMasters/Edit/5
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
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.refStateId = new SelectList(db.StateMasters, "StateId", "StateName", cityMaster.refStateId);
            return View(cityMaster);
        }

        // POST: admin/CityMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityId,CityName,refStateId")] CityMaster cityMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(cityMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.refStateId = new SelectList(db.StateMasters, "StateId", "StateName", cityMaster.refStateId);
            return View(cityMaster);
        }

        // GET: admin/CityMasters/Delete/5
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
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            return View(cityMaster);
        }

        // POST: admin/CityMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            CityMaster cityMaster = db.CityMasters.Find(id);
            db.CityMasters.Remove(cityMaster);
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
