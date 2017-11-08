using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Club;
using Club.Models;

namespace Club.Controllers
{
    public class HomeController : Controller
    {
        public static string HMACSHA1Encrypt(string EncryptText, string EncryptKey)
        {
            byte[] StrRes = Encoding.Default.GetBytes(EncryptText);
            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.Default.GetBytes(EncryptKey));
            CryptoStream CStream = new CryptoStream(Stream.Null, myHMACSHA1, CryptoStreamMode.Write);
            CStream.Write(StrRes, 0, StrRes.Length);
            StringBuilder EnText = new StringBuilder();
            foreach (byte Byte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", Byte);
            }
            return EnText.ToString();
        }

        /// <summary>
        /// HMAC-SHA1加密算法
        /// </summary>
        /// <param name="secret">密钥</param>
        /// <param name="strOrgData">源文</param>
        /// <returns></returns>
        public static string HmacSha1Sign(string secret, string strOrgData)
        {
            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(secret));
            var dataBuffer = Encoding.UTF8.GetBytes(strOrgData);
            var hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Base64编码类。
        /// 将byte[]类型转换成Base64编码的string类型。
        /// </summary>
        public class Base64Encoder
        {
            byte[] source;
            int length, length2;
            int blockCount;
            int paddingCount;
            public static Base64Encoder Encoder = new Base64Encoder();

            public Base64Encoder()
            {
            }

            private void init(byte[] input)
            {
                source = input;
                length = input.Length;
                if ((length % 3) == 0)
                {
                    paddingCount = 0;
                    blockCount = length / 3;
                }
                else
                {
                    paddingCount = 3 - (length % 3);
                    blockCount = (length + paddingCount) / 3;
                }
                length2 = length + paddingCount;
            }

            public string GetEncoded(byte[] input)
            {
                //初始化
                init(input);

                byte[] source2;
                source2 = new byte[length2];

                for (int x = 0; x < length2; x++)
                {
                    if (x < length)
                    {
                        source2[x] = source[x];
                    }
                    else
                    {
                        source2[x] = 0;
                    }
                }

                byte b1, b2, b3;
                byte temp, temp1, temp2, temp3, temp4;
                byte[] buffer = new byte[blockCount * 4];
                char[] result = new char[blockCount * 4];
                for (int x = 0; x < blockCount; x++)
                {
                    b1 = source2[x * 3];
                    b2 = source2[x * 3 + 1];
                    b3 = source2[x * 3 + 2];

                    temp1 = (byte)((b1 & 252) >> 2);

                    temp = (byte)((b1 & 3) << 4);
                    temp2 = (byte)((b2 & 240) >> 4);
                    temp2 += temp;

                    temp = (byte)((b2 & 15) << 2);
                    temp3 = (byte)((b3 & 192) >> 6);
                    temp3 += temp;

                    temp4 = (byte)(b3 & 63);

                    buffer[x * 4] = temp1;
                    buffer[x * 4 + 1] = temp2;
                    buffer[x * 4 + 2] = temp3;
                    buffer[x * 4 + 3] = temp4;

                }

                for (int x = 0; x < blockCount * 4; x++)
                {
                    result[x] = sixbit2char(buffer[x]);
                }


                switch (paddingCount)
                {
                    case 0: break;
                    case 1: result[blockCount * 4 - 1] = '='; break;
                    case 2:
                        result[blockCount * 4 - 1] = '=';
                        result[blockCount * 4 - 2] = '=';
                        break;
                    default: break;
                }
                return new string(result);
            }
            private char sixbit2char(byte b)
            {
                char[] lookupTable = new char[64]{
                  'A','B','C','D','E','F','G','H','I','J','K','L','M',
                 'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                 'a','b','c','d','e','f','g','h','i','j','k','l','m',
                 'n','o','p','q','r','s','t','u','v','w','x','y','z',
                 '0','1','2','3','4','5','6','7','8','9','+','/'};

                if ((b >= 0) && (b <= 63))
                {
                    return lookupTable[(int)b];
                }
                else
                {

                    return ' ';
                }
            }

        }

      

        public ActionResult Index()
        {
            var secretString= HmacSha1Sign("test_secret_key", "contentTest");

            HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes("test_secret_key"));
            byte[] rstRes = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes("contentTest"));
            string strs = Convert.ToBase64String(rstRes);
           
             
            byte[] bytes = Encoding.Default.GetBytes(secretString);
            string secret = Convert.ToBase64String(bytes);

            Base64Encoder base64Encoder=new Base64Encoder();
            var key=  base64Encoder.GetEncoded(bytes);

            var dt = DateTime.Now.AddMonths(-1);

            var dttime = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);

            var dayofWeek = dt.DayOfWeek;
            int dayOfWeek = Convert.ToInt32(dayofWeek.ToString("d"));

            if (dayOfWeek == 0)
                dayOfWeek = 7;

            var beginOfWeek = dttime.AddDays(1 - dayOfWeek);
            var endOfWeek = beginOfWeek.AddDays(6);

            var beginOfMonth=new DateTime(dt.Year,dt.Month,1,0,0,0,0,0);

            var bm = dttime.AddDays(1 - dttime.Day);
            var endOfMonth = bm.AddMonths(1).AddDays(-1);


            var loginUser = (User)Session["loginUser"];
            ViewBag.LoginUser = loginUser;

            var cookies = new HttpCookie("User");
            using (var db = new ClubEntities())
            {
                var postList = new List<ListPostModel>();
                var list = db.Post.ToList();

                foreach (var item in list)
                {
                    var postModel = new ListPostModel();
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