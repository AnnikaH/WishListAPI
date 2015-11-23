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
    public class WishListHelperController : ApiController
    {
        DBWishList dbWishList = new DBWishList();

        // GET api/WishListHelper/5
        public HttpResponseMessage GetAllWishListsForUser(int id)
        {
            List<WishList> allWishListsForUser = dbWishList.GetAllWishListsForUser(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allWishListsForUser);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}