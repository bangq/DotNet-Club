using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Club;
using Club.Models;

namespace Club.Controllers
{
    public class HomeController : Controller
    {
        [AuthFilter]
        public ActionResult Index()
        {

            var loginUser= (User)Session["loginUser"];
            ViewBag.LoginUser = loginUser;

            var cookies=new HttpCookie("User");
            using (var db=new ClubEntities())
            {
                var postList=new List<ListPostModel>();
                var list = db.Post.ToList();

                foreach (var item in list)
                {
                    var postModel=new ListPostModel();
                    postModel.Id = item.Id;
                    postModel.Title = item.Title;
                    postModel.CreateTime = item.CreateTime;
                    postModel.IsFeatured = item.IsFeatured;
                    postModel.UserName = item.User.Account;
                    postModel.ViewCount = item.ViewCount;

                    postList.Add(postModel);
                }

                return View(postList);
            }
             
        }

        
    }
}