using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Club.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }
       
        public ActionResult Login()
        {
            return View();
        }
    }
}