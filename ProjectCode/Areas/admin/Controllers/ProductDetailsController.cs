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
    public class ProductDetailsController : Controller
    {
        private dbOPIEntities db = new dbOPIEntities();

        // GET: admin/ProductDetails
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            var productMasters = db.ProductMasters.Include(p => p.CategoryMaster).Include(p => p.UserMaster);
            return View(productMasters.ToList());
        }

        // GET: admin/ProductDetails/Details/5
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
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            return View(productMaster);
        }

        // GET: admin/ProductDetails/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            ViewBag.refCategory = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.OwnerId = new SelectList(db.UserMasters, "UserId", "UserName");
            return View();
        }

        // POST: admin/ProductDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Title,MyPrice,OriginalPrice,IsSecondHand,OwnerId,UploadedDate,Desccription,IsSold,ImageUrl,refCategory")] ProductMaster productMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.ProductMasters.Add(productMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.refCategory = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", productMaster.refCategory);
            ViewBag.OwnerId = new SelectList(db.UserMasters, "UserId", "UserName", productMaster.OwnerId);
            return View(productMaster);
        }

        // GET: admin/ProductDetails/Edit/5
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
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.refCategory = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", productMaster.refCategory);
            ViewBag.OwnerId = new SelectList(db.UserMasters, "UserId", "UserName", productMaster.OwnerId);
            return View(productMaster);
        }

        // POST: admin/ProductDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Title,MyPrice,OriginalPrice,IsSecondHand,OwnerId,UploadedDate,Desccription,IsSold,ImageUrl,refCategory")] ProductMaster productMaster)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(productMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.refCategory = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", productMaster.refCategory);
            ViewBag.OwnerId = new SelectList(db.UserMasters, "UserId", "UserName", productMaster.OwnerId);
            return View(productMaster);
        }

        // GET: admin/ProductDetails/Delete/5
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
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            return View(productMaster);
        }

        // POST: admin/ProductDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            ProductMaster productMaster = db.ProductMasters.Find(id);
            db.ProductMasters.Remove(productMaster);
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
