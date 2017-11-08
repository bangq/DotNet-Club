using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Club.Models
{
    public class GeoItemModel
    {
        /// <summary>
        /// 位置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 位置经纬度
        /// </summary>
        public string LongitudeAndLatitude { get; set; }



    }

    public class GeoSearchModel
    {

        /// <summary>
        /// 位置经纬度
        /// </summary>
        public string LongitudeAndLatitude { get; set; }

        /// <summary>
        /// 半径范围
        /// </summary>
        public double Radius { get; set; }



    }
}