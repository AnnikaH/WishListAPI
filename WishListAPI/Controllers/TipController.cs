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
    public class TipController : ApiController
    {
        DBWishList dbWishList = new DBWishList();
        
        // GET api/Tip
        public HttpResponseMessage GetAllWishTips()
        {
            List<WishTip> allWishTips = dbWishList.GetAllWishTips();

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allWishTips);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        // GET api/Tip/5
        public HttpResponseMessage Get(int id)
        {
            WishTip oneWishTip = dbWishList.GetWishTip(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(oneWishTip);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        // POST api/Tip
        public HttpResponseMessage Post(WishTip wishTip)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.CreateWishTip(wishTip);

                if (ok)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Kunne ikke sette inn dette tipset i databasen.")
            };
        }

        // PUT api/Tip/5
        public HttpResponseMessage Put(int id, [FromBody]WishTip wishTip)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.UpdateWishTip(id, wishTip);

                if (ok)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Kunne ikke endre tips med id " + id + " i databasen.")
            };
        }

        // DELETE api/Tip/5
        public HttpResponseMessage Delete(int id)
        {
            bool ok = dbWishList.DeleteWishTip(id);

            if (!ok)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Kunne ikke slette tips med id " + id + " fra databasen.")
                };
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}