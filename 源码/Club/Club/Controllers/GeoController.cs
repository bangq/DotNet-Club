using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Club.Models;
using StackExchange.Redis;

namespace Club.Controllers
{
    public class GeoController : BaseController
    {
        // GET: Geo
        public ActionResult Index()
        {
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("47.93.184.106:6379");

            //redis.GetDatabase(1).StringSet("test.user:1", "user1");

            return View();
        }
        [HttpPost]
        public ActionResult Add(GeoItemModel geoItemModel)
        {


            var lls = geoItemModel.LongitudeAndLatitude.Split(',');
            var longitude =double.Parse(lls[0]);
            var latitude = double.Parse(lls[1]);
            var geo = new GeoEntry(longitude, latitude, geoItemModel.Name);


            var geoKey = "shop_geo_key";

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("47.93.184.106:6379");
            redis.GetDatabase(1).GeoAdd(geoKey, geo);
            return Content("Ok");

        }

     
        [HttpPost]
        public ActionResult Radius(GeoSearchModel geoSearchModel)
        {
            var geoKey = "shop_geo_key";

            var result = "";
            var lls = geoSearchModel.LongitudeAndLatitude.Split(',');
            var longitude =double.Parse(lls[0]);
            var latitude = double.Parse(lls[1]);
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("47.93.184.106:6379");

            var list = redis.GetDatabase(1).GeoRadius(geoKey, longitude, latitude, geoSearchModel.Radius).OrderBy(a => a.Distance);

            var sb = new StringBuilder();

            sb.AppendLine("当前坐标:" + geoSearchModel.LongitudeAndLatitude + "   搜索半径:" + geoSearchModel.Radius);
            sb.AppendLine("</br>");
            foreach (var item in list)
            {
                var str = "位置：" + item.Member + "距离：" + item.Distance + "-------------------(坐标：" + item.Position + ")";
                sb.AppendLine(str);
                sb.AppendLine("</br>");

            }

            return Content(sb.ToString());
        }
    }
}