using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Club.Api.Models
{
    public class UserModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
    }
}