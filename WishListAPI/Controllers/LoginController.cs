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
    public class LoginController : ApiController
    {
        DBWishList dbWishList = new DBWishList();

        // GET
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Du har ikke tilgang.")
            };
        }

        // GET
        public HttpResponseMessage Get(int id)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Du har ikke tilgang.")
            };
        }

        // POST api/Login
        public HttpResponseMessage Post(LoginUser loginUser)
        {
            //LoginUser loginUser = new LoginUser();
            //loginUser.userName = userName;
            //loginUser.password = password;

            //bool ok = dbWishList.UserInDb(loginUser);

            //return ok;

            if (ModelState.IsValid)
            {
                bool ok = dbWishList.UserInDb(loginUser);

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
                Content = new StringContent("Kunne ikke finne denne brukeren i databasen.")
            };
        }

        // PUT
        public HttpResponseMessage Put(int id, [FromBody]LoginUser loginUser)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Du har ikke tilgang.")
            };
        }

        // DELETE
        public HttpResponseMessage Delete(int id)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Du har ikke tilgang.")
            };
        }
    }
}