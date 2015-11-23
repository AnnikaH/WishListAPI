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
    public class WishHelperController : ApiController
    {
        DBWishList dbWishList = new DBWishList();

        // GET api/WishHelper/5
        public HttpResponseMessage GetAllWishesForList(int id)
        {
            List<Wish> allWishesForList = dbWishList.GetAllWishesForList(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allWishesForList);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}