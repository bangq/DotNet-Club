using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Club.Api.Models;

namespace Club.Api.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public List<UserModel> List()
        {
            var list=new List<UserModel>();

            for (int i = 0; i < 10; i++)
            {
                var model=new UserModel();
                model.Id = i;
                model.Account = "测试账号" + i;
                model.Name = "name" + i;
                list.Add(model);
            }

            return list;
        } 
    }
}
