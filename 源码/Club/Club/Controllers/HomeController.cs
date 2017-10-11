using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Club;

namespace Club.Controllers
{
    public class HomeController : Controller
    {
        [AuthFilter]
        public ActionResult Index()
        {

            var loginUser= (User)Session["loginUser"];
            ViewBag.LoginUser = loginUser;
            return View();
        }

        
    }
}