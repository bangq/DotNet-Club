using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Club.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
              
            //using (var db = new ClubEntities())
            //{
            //    var level = new Level();
            //    level.Name = "级别1";

            //    db.Level.Add(level);
            //    db.SaveChanges();
            //}


            //using (var db=new ClubEntities())
            //{
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        var user = new User();
            //        user.LevelId = 1;
            //        user.Name = "user"+i;
            //        user.Account = "user"+i;
            //        user.PassWord = "000000";
            //        db.User.Add(user);
            //        db.SaveChanges();
            //    }
                
            //}

            //using (var db= new ClubEntities())
            //{
            //    var user = db.User.FirstOrDefault(a => a.Id == 2);
            //    if (user != null)
            //    {
            //        db.User.Remove(user);
            //        db.SaveChanges();
            //    }
                
            //}

            //using (var db=new ClubEntities())
            //{

            //    var user = db.User.FirstOrDefault(a => a.Id == 3);
            //    if (user != null)
            //    {
            //        user.Name = "软件153的用户";
            //    }
            //    db.SaveChanges();
            //}

            using (var db=new ClubEntities())
            {

                var users = db.User.Where(t => t.Id < 10).ToList();

                var sb=new StringBuilder();
                foreach (var item in users)
                {
                    sb.AppendLine("用户名：" + item.Name+"用户级别："+item.Level.Name);
                }
                return Content(sb.ToString());

            }


            return Content("ok");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}