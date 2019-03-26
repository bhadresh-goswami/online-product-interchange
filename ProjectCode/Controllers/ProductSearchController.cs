using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectCode.Models;
using System.IO;

namespace ProjectCode.Controllers
{
    public class ProductSearchController : Controller
    {
        private dbOPIEntities db = new dbOPIEntities();

        // GET: ProductSearch
        public ActionResult Index(string desc = "", string category = "", decimal price = 0)
        {
            if (category != "" && price != 0 && desc != "")
            {

                var productMastersSearch = db.ProductMasters.
                    Include(p => p.CategoryMaster).
                    Include(p => p.UserMaster).Where(
                    p => p.CategoryMaster.CategoryName.Contains(category)
                    &&
                    p.MyPrice <= price
                    && (
                    p.Title.Contains(desc)
                    ||
                    p.Desccription.Contains(desc)
                    )
                    );
                return View(productMastersSearch.ToList());
            }
            else if (category != "")
            {

                var productMastersSearch = db.ProductMasters.
                    Include(p => p.CategoryMaster).
                    Include(p => p.UserMaster).Where(
                    p => p.CategoryMaster.CategoryName.Contains(category)

                    );
                return View(productMastersSearch.ToList());
            }
            else if (desc != "")
            {
                var productMastersSearch = db.ProductMasters.
                    Include(p => p.CategoryMaster).
                    Include(p => p.UserMaster).Where(
                    p =>
                    p.Title.Contains(desc)
                    ||
                    p.Desccription.Contains(desc)
                    );
                return View(productMastersSearch.ToList());
            }
            else if (price != 0)
            {

                var productMastersSearch = db.ProductMasters.
                    Include(p => p.CategoryMaster).
                    Include(p => p.UserMaster).Where(
                    p =>
                    p.MyPrice <= price
                    );
                return View(productMastersSearch.ToList());
            }

            var productMasters = db.ProductMasters.Include(p => p.CategoryMaster).Include(p => p.UserMaster);
            return View(productMasters.ToList());
        }


        // GET: ProductSearch/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: ProductSearch/Create
        public ActionResult Create()
        {
            ViewBag.refCategory = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.OwnerId = new SelectList(db.UserMasters, "UserId", "UserName");
            return View();
        }

        // POST: ProductSearch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Title,MyPrice,OriginalPrice,IsSecondHand,OwnerId,UploadedDate,Desccription,IsSold,ImageUrl,refCategory")] ProductMaster productMaster, HttpPostedFileBase file)
        {
            try
            {

                if (file == null)
                {
                    TempData["error"] = "Please Select File!";
                    return RedirectToAction("NewUser");
                }
                string dir = Server.MapPath("~/Content/user/ProductImage/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string fpath = dir + "/" + file.FileName;
                file.SaveAs(fpath);

                productMaster.ImageUrl = file.FileName;


                productMaster.OwnerId = int.Parse(Session["user"].ToString());
                db.ProductMasters.Add(productMaster);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {



            }
            return RedirectToAction("Index");

        }

        // GET: ProductSearch/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: ProductSearch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Title,MyPrice,OriginalPrice,IsSecondHand,OwnerId,UploadedDate,Desccription,IsSold,ImageUrl,refCategory")] ProductMaster productMaster)
        {
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

        // GET: ProductSearch/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: ProductSearch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
