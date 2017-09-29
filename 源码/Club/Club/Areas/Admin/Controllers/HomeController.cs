using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Club.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var user = (User)Session["loginUser"];
            if (user == null)
            {
                return Redirect("/admin/login");
            }

            return View();
        }
    }
}