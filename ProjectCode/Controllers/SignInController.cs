using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProjectCode.Models;
using System.IO;

namespace ProjectCode.Controllers
{
    public class SignInController : CommonCodeController
    {

        #region UserSignIn

        // GET: SignIn
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexProcess(UserMaster model)
        {
            try
            {
                var data = db.UserMasters.SingleOrDefault(a => a.UserEmailId == model.UserEmailId &&
                a.UserPassword == model.UserPassword);

                if (data != null)
                {
                    Session["user"] = data.UserId;
                    return RedirectToAction("Index", "Home");
                }
                TempData["error"] = "User Name or password is wrong!";
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
            }
            return View();
        }
        #endregion
        #region UserSignUp

        // GET: SignIn
        public ActionResult NewUser()
        {
            ViewBag.refCountryId = new SelectList(db.CountryMasters, "CountryId", "CountryName");
            ViewBag.refStateId = new SelectList(db.StateMasters, "StateId", "StateName");

            ViewBag.refCityId = new SelectList(db.CityMasters, "CityId", "CityName");
            return View();
        }
        [HttpPost]
        [ActionName("NewUser")]
        public ActionResult NewUserAdd(UserMaster model, HttpPostedFileBase file)
        {
            ViewBag.refCountryId = new SelectList(db.CountryMasters, "CountryId", "CountryName");
            ViewBag.refStateId = new SelectList(db.StateMasters, "StateId", "StateName");
            try
            {

                ViewBag.refCityId = new SelectList(db.CityMasters, "CityId", "CityName");
                if (file == null)
                {
                    TempData["error"] = "Please Select File!";
                    return RedirectToAction("NewUser");
                }
                string dir = Server.MapPath("~/Content/user/userImage/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string fpath = dir + "/" + file.FileName;
                file.SaveAs(fpath);

                model.ImageUrl = file.FileName;
                var r = new Random();
                var pass = "pass_" + r.Next(100, 87878).ToString();
                model.IsBlocked = false;
                model.RegistrationDate = DateTime.Now.Date;
                model.UserPassword = pass;

                db.UserMasters.Add(model);
                db.SaveChanges();

                TempData["message"] = "User Registration Done!";
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
            }
            return RedirectToAction("NewUser");
        }
        #endregion


        #region ExpertSignIn

        // GET: SignIn
        public ActionResult ExistingExpert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExistingExpertProcess()
        {
            return View();
        }
        #endregion
        #region ExpertSignUp

        // GET: SignIn
        public ActionResult NewExpert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewExpertAdd()
        {
            return View();
        }
        #endregion

        public ActionResult UpdateProfile()
        {
            if(Session["user"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.refCountryId = new SelectList(db.CountryMasters, "CountryId", "CountryName");
            ViewBag.refStateId = new SelectList(db.StateMasters, "StateId", "StateName");

            ViewBag.refCityId = new SelectList(db.CityMasters, "CityId", "CityName");
            int uid = int.Parse(Session["user"].ToString());
            var data = db.UserMasters.SingleOrDefault(a => a.UserId == uid);

            return View(data);
        }
        [HttpPost]
        public ActionResult UpdateProfile(UserMaster model, HttpPostedFileBase file)
        {
            ViewBag.refCountryId = new SelectList(db.CountryMasters, "CountryId", "CountryName");
            ViewBag.refStateId = new SelectList(db.StateMasters, "StateId", "StateName");
            try
            {

                ViewBag.refCityId = new SelectList(db.CityMasters, "CityId", "CityName");
                if (file == null)
                {
                    TempData["error"] = "Please Select File!";
                    return RedirectToAction("NewUser");
                }
                string dir = Server.MapPath("~/Content/user/userImage/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string fpath = dir + "/" + file.FileName;
                file.SaveAs(fpath);

                model.ImageUrl = file.FileName;
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                TempData["message"] = "User Update Done!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
            }
            return RedirectToAction("NewUser");
        }


    }
}