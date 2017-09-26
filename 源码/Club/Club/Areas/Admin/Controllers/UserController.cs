using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Webdiyer.WebControls.Mvc;
using Club;

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
                 list = db.User.Where(a=>a.IsAbort==false).OrderByDescending(a=>a.Id).Include(a=>a.Level).ToPagedList(pageIndex:pageIndex,pageSize:pageSize);
            }

            return View(list);
        }

        public ActionResult Delete()
        {
            var idStr = Request["Id"];
            var id = idStr.ToInt();
            if (id == 0)
            {
                TempData["Msg"] = "数据不正确！";

                return RedirectToAction("Index");
            }
             
            using (var db=new ClubEntities())
            {
                var user = db.User.FirstOrDefault(a => a.Id == id);
                if (user != null)
                {
                    user.IsAbort = true;
                    //db.User.Remove(user);
                    db.SaveChanges();
                }
            }

            TempData["Msg"] = "删除成功！";

            return RedirectToAction("Index");
        }

        
    }

   
}