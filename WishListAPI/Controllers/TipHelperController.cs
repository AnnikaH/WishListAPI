using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WishListAPI.Models;
using System.Web.Script.Serialization;
using System.Net.Http.Formatting;
using System.Data.Common;
using System.Text;

namespace WishListAPI.Controllers
{
    public class TipHelperController : ApiController
    {
        DBWishList dbWishList = new DBWishList();

        /* GET api/TipHelper/5
        public HttpResponseMessage GetAllWishTipsForSender(int id)
        {
            List<WishTip> allWishTipsForSender = dbWishList.GetAllWishTipsForSender(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allWishTipsForSender);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }*/

        // GET api/TipHelper/5
        public HttpResponseMessage GetAllWishTipsForReceiver(int id)
        {
            List<WishTip> allWishTipsForReceiver = dbWishList.GetAllWishTipsForReceiver(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allWishTipsForReceiver);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}