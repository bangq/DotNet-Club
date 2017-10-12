using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Club.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [WebAuthFilter(IsNeedLogin = true)]
        public ActionResult New()
        {
            using (var db=new ClubEntities())
            {
                var categorys = db.Category.ToList();
                ViewBag.Categorys = categorys;
            }
            return View();
        }
    }
}