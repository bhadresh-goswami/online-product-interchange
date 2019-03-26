using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCode.Models;

namespace ProjectCode.Controllers
{
    public class CommonCodeController : Controller
    {
        protected dbOPIEntities db = new dbOPIEntities();
        protected UserMaster userMaster = new UserMaster();
        protected CountryMaster countryObj = new CountryMaster();
        protected StateMaster stateObj = new StateMaster();
        protected CityMaster cityObj = new CityMaster();

    }
}