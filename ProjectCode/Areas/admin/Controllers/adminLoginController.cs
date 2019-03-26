using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProjectCode.Models;

namespace ProjectCode.Areas.admin.Controllers
{
    public class adminLoginController : Controller
    {
        dbOPIEntities db = new dbOPIEntities();
        // GET: admin/adminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string LoginEmailId,string LoginPassword)
        {
            try
            {
                if(db.AdminLoginMasters.SingleOrDefault(a=>a.LoginEmailId==LoginEmailId && a.LoginPassword ==LoginPassword) == null)
                {
                    TempData["Error"] = "Admin Email Id Or Password is Wrong!";
                }
                else
                {
                    Session["admin"] = LoginEmailId;
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                
            }
            return View();
        }
    }
}