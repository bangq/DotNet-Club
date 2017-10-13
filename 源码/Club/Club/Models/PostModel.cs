using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Club.Models
{
    public class ListPostModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 帖子标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发帖人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 是否精华帖子
        /// </summary>
        public bool IsFeatured { get; set; }


    }
}