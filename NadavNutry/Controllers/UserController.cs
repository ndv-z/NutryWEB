using NadavNutry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NadavNutry.Controllers
{
    public class UserController : Controller
    {
        DBModel DataBase = new DBModel();

        // GET: User
        public ActionResult Index()
        {

           //System.Diagnostics.Debug.WriteLine(jsonRes);
            return Content("sssss");
        }
    }
}