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
    public class SharingController : ApiController
    {
        DBWishList dbWishList = new DBWishList();
        
        // GET api/Sharing
        public HttpResponseMessage Get()
        {
            List<Sharing> allSharings = dbWishList.GetAllSharings();

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allSharings);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
        
        // GET api/Sharing/5
        public HttpResponseMessage Get(int id)
        {
            Sharing oneSharing = dbWishList.GetSharing(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(oneSharing);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        // POST api/Sharing
        public HttpResponseMessage Post(Sharing sharing)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.CreateSharing(sharing);

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
                Content = new StringContent("Kunne ikke sette inn denne delingen i databasen.")
            };
        }

        // PUT api/Sharing/5
        public HttpResponseMessage Put(int id, [FromBody]Sharing sharing)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.UpdateSharing(id, sharing);

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
                Content = new StringContent("Kunne ikke endre deling med id " + id + " i databasen.")
            };
        }

        // DELETE api/Sharing/5
        public HttpResponseMessage Delete(int id)
        {
            bool ok = dbWishList.DeleteSharing(id);

            if (!ok)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Kunne ikke slette deling med id " + id + " fra databasen.")
                };
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}