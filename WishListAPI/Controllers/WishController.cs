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
    public class WishController : ApiController
    {
        DBWishList dbWishList = new DBWishList();

        /*
        // GET api/Wish/GetAllWishes
        public HttpResponseMessage GetAllWishes()
        {
            List<Wish> allWishes = dbWishList.GetAllWishes();

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allWishes);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
        */

        // GET api/Wish/Get/5
        public HttpResponseMessage Get(int id)
        {
            Wish oneWish = dbWishList.GetWish(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(oneWish);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        // POST api/Wish
        public HttpResponseMessage Post(Wish wish)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.CreateWish(wish);

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
                Content = new StringContent("Kunne ikke sette inn dette ønsket i databasen.")
            };
        }

        // PUT api/Wish/5
        public HttpResponseMessage Put(int id, [FromBody]Wish wish)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.UpdateWish(id, wish);

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
                Content = new StringContent("Kunne ikke endre ønske med id " + id + " i databasen.")
            };
        }

        // DELETE api/Wish/5
        public HttpResponseMessage Delete(int id)
        {
            bool ok = dbWishList.DeleteWish(id);

            if (!ok)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Kunne ikke slette ønske med id " + id + " fra databasen.")
                };
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}