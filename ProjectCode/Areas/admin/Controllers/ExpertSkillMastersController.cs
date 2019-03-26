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
    public class ExpertSkillMastersController : Controller
    {
        private dbOPIEntities db = new dbOPIEntities();

        // GET: admin/ExpertSkillMasters
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            var expertSkillMasters = db.ExpertSkillMasters.Include(e => e.CategoryMaster).Include(e => e.ExpertMaster);
            return View(expertSkillMasters.ToList());
        }

        // GET: admin/ExpertSkillMasters/Details/5
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
            ExpertSkillMaster expertSkillMaster = db.ExpertSkillMasters.Find(id);
            if (expertSkillMaster == null)
            {
                return HttpNotFound();
            }
            return View(expertSkillMaster);
        }

        // GET: admin/ExpertSkillMasters/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            ViewBag.refCategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.refExpertId = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName");
            return View();
        }

        // POST: admin/ExpertSkillMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpertSkillId,refExpertId,refCategoryId")] ExpertSkillMaster expertSkillMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.ExpertSkillMasters.Add(expertSkillMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.refCategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", expertSkillMaster.refCategoryId);
            ViewBag.refExpertId = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", expertSkillMaster.refExpertId);
            return View(expertSkillMaster);
        }

        // GET: admin/ExpertSkillMasters/Edit/5
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
            ExpertSkillMaster expertSkillMaster = db.ExpertSkillMasters.Find(id);
            if (expertSkillMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.refCategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", expertSkillMaster.refCategoryId);
            ViewBag.refExpertId = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", expertSkillMaster.refExpertId);
            return View(expertSkillMaster);
        }

        // POST: admin/ExpertSkillMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpertSkillId,refExpertId,refCategoryId")] ExpertSkillMaster expertSkillMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(expertSkillMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.refCategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", expertSkillMaster.refCategoryId);
            ViewBag.refExpertId = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", expertSkillMaster.refExpertId);
            return View(expertSkillMaster);
        }

        // GET: admin/ExpertSkillMasters/Delete/5
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
            ExpertSkillMaster expertSkillMaster = db.ExpertSkillMasters.Find(id);
            if (expertSkillMaster == null)
            {
                return HttpNotFound();
            }
            return View(expertSkillMaster);
        }

        // POST: admin/ExpertSkillMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            ExpertSkillMaster expertSkillMaster = db.ExpertSkillMasters.Find(id);
            db.ExpertSkillMasters.Remove(expertSkillMaster);
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
