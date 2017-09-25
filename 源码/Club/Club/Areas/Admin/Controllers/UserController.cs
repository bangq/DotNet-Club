using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace Club.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            int pageSize = 10;
            var indexStr = Request["pageIndex"];
            if (string.IsNullOrEmpty(indexStr))
            {
                indexStr = "1";
            }

            var pageIndex = int.Parse(indexStr);


            var list=new List<User>();
            using (var db=new ClubEntities())
            {
                 list = db.User.OrderByDescending(a=>a.Id).Include(a=>a.Level).ToPagedList(pageIndex:pageIndex,pageSize:pageSize);
            }

            return View(list);
        }
    }
}