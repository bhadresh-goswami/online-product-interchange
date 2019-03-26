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
    public class CategoryMastersController : Controller
    {
        private dbOPIEntities db = new dbOPIEntities();

        // GET: admin/CategoryMasters
        public ActionResult Index()
        {

            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            return View(db.CategoryMasters.ToList());
        }

        // GET: admin/CategoryMasters/Details/5
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
            CategoryMaster categoryMaster = db.CategoryMasters.Find(id);
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(categoryMaster);
        }

        // GET: admin/CategoryMasters/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            return View();
        }

        // POST: admin/CategoryMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] CategoryMaster categoryMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.CategoryMasters.Add(categoryMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryMaster);
        }

        // GET: admin/CategoryMasters/Edit/5
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
            CategoryMaster categoryMaster = db.CategoryMasters.Find(id);
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(categoryMaster);
        }

        // POST: admin/CategoryMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] CategoryMaster categoryMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(categoryMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryMaster);
        }

        // GET: admin/CategoryMasters/Delete/5
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
            CategoryMaster categoryMaster = db.CategoryMasters.Find(id);
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(categoryMaster);
        }

        // POST: admin/CategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            CategoryMaster categoryMaster = db.CategoryMasters.Find(id);
            db.CategoryMasters.Remove(categoryMaster);
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
