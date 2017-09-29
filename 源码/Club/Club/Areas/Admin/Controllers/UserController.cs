using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Webdiyer.WebControls.Mvc;
using Club;

namespace Club.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
             
            int pageSize = 10;
            var indexStr = Request["pageIndex"];
            var kw = Request["kw"];
            if (string.IsNullOrEmpty(indexStr))
            {
                indexStr = "1";
            }

            var pageIndex = int.Parse(indexStr);

            IPagedList<User> items ;
            
            using (var db = new ClubEntities())
            {
                var list = db.User.Where(a => a.IsAbort == false).Include(a => a.Level);

                if (!string.IsNullOrEmpty(kw))
                {
                    list = list.Where(a => a.Account.Contains(kw) || a.Name.Contains(kw));
                    ViewBag.KW = kw;
                }

                items = list.OrderByDescending(a => a.Id).ToPagedList(pageIndex: pageIndex, pageSize: pageSize);
            }

            return View(items);
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

            using (var db = new ClubEntities())
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

        [HttpGet]
        public ActionResult Edit()
        {
            
            var Id = Request["Id"].ToInt();

            using (var db = new ClubEntities())
            {
                var user = db.User.Include(a => a.Level).FirstOrDefault(a => a.Id == Id);


                var selectItems = new List<SelectListItem>();

                var levels = db.Level.ToList();

                foreach (var level in levels)
                {
                    var selectItem = new SelectListItem();
                    selectItem.Text = level.Name;
                    selectItem.Value = level.Id.ToString();
                    if (user != null && (user.LevelId == level.Id))
                    {
                        selectItem.Selected = true;
                    }
                    selectItems.Add(selectItem);
                }

                ViewBag.SeletItems = selectItems;

                if (user == null)
                    user = new User();
                return View(user);
            }

        }
       
        [HttpPost]
        public ActionResult Save()
        {
            
            var id = Request["id"].ToInt();
            var name = Request["name"];
            var account = Request["account"];
            var levelId = Request["levelId"].ToInt();
            var integral = Request["integral"].ToInt();
            var image = Request["image"];


            using (var db = new ClubEntities())
            {
                var user = db.User.FirstOrDefault(a => a.Id == id);

                if (user == null)
                {
                    user = new User();
                    user.Account = account;
                    user.IsAdmin = false;
                    user.PassWord = "000000";
                    db.User.Add(user);
                }

                user.Name = name;
                user.LevelId = levelId;
                user.integral = integral;
                user.Image = image;
                db.SaveChanges();

                ShowMassage("操作成功");
            }


            //if (id == 0)
            //{
            //    using (var db = new ClubEntities())
            //    {
            //        var user = new User();
            //        user.Name = name;
            //        user.Account = account;
            //        user.LevelId = levelId;
            //        user.integral = integral;
            //        user.Image = image;
            //        user.IsAdmin = false;
            //        user.PassWord = "000000";

            //        db.User.Add(user);

            //        db.SaveChanges();

            //        ShowMassage("新增成功");
            //    }
            //}





            return RedirectToAction("Index");
        }


    }


}