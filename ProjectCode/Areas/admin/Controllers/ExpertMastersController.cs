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
    public class ExpertMastersController : Controller
    {
        private dbOPIEntities db = new dbOPIEntities();

        // GET: admin/ExpertMasters
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            return View(db.ExpertMasters.ToList());
        }

        // GET: admin/ExpertMasters/Details/5
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
            ExpertMaster expertMaster = db.ExpertMasters.Find(id);
            if (expertMaster == null)
            {
                return HttpNotFound();
            }
            return View(expertMaster);
        }

        // GET: admin/ExpertMasters/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            return View();
        }

        // POST: admin/ExpertMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpertId,ExpertName,ExpertPassword,ExpertEmailId,ExpertMobile,Occupation,Price")] ExpertMaster expertMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.ExpertMasters.Add(expertMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expertMaster);
        }

        // GET: admin/ExpertMasters/Edit/5
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
            ExpertMaster expertMaster = db.ExpertMasters.Find(id);
            if (expertMaster == null)
            {
                return HttpNotFound();
            }
            return View(expertMaster);
        }

        // POST: admin/ExpertMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpertId,ExpertName,ExpertPassword,ExpertEmailId,ExpertMobile,Occupation,Price")] ExpertMaster expertMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(expertMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expertMaster);
        }

        // GET: admin/ExpertMasters/Delete/5
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
            ExpertMaster expertMaster = db.ExpertMasters.Find(id);
            if (expertMaster == null)
            {
                return HttpNotFound();
            }
            return View(expertMaster);
        }

        // POST: admin/ExpertMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            ExpertMaster expertMaster = db.ExpertMasters.Find(id);
            db.ExpertMasters.Remove(expertMaster);
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
