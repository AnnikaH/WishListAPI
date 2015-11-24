﻿using System;
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
    public class SharingHelperController : ApiController
    {
        DBWishList dbWishList = new DBWishList();

        // GET all sharings from userId:
        // GET api/SharingHelper/5
        public HttpResponseMessage GetAllSharingsFromUserId(int id)
        {
            List<Sharing> sharings = dbWishList.GetAllSharingsFromUserId(id);
            
            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(sharings);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
