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
    public class UserHelperController : ApiController
    {
        DBWishList dbWishList = new DBWishList();
        
        /*
        public HttpResponseMessage GetUserByUserName(String name)
        {
            User oneUser = dbWishList.GetUserByUserName(name);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(oneUser);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }*/
    }
}