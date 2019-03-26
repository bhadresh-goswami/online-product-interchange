using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCode.Areas.admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: admin/Dashboard
        public ActionResult Index()
        {

            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "adminLogin");
            }
            return View();
        }
        public ActionResult logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "adminLogin");
        }
    }
}