using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ProjectCode.Models;

namespace ProjectCode.Controllers
{
    public class HomeController : Controller
    {

        dbOPIEntities db = new dbOPIEntities();
        #region Other
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        #endregion


        public JsonResult GetStates(int id)
        {
            return Json(db.StateMasters.Where(a => a.refCountryId == id).Select(n => new { n.StateId, n.StateName }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCity(int id)
        {
            return Json(db.CityMasters.Where(a => a.refStateId == id).Select(n => new { n.CityId, n.CityName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

    }
}