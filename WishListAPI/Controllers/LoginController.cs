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

        // POST api/Login
        public HttpResponseMessage Post(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                User user = dbWishList.GetUserByLogin(loginUser);

                if (user != null)
                {
                    /*LoginUser loginUser = new LoginUser();
                    loginUser.userName = user.userName;
                    loginUser.password = user.password;
                    User createdUser = dbWishList.GetUserByLogin(loginUser);

                    var Json = new JavaScriptSerializer();
                    string JsonString = Json.Serialize(createdUser);

                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                        StatusCode = HttpStatusCode.OK
                    };*/

                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("" + user.id)
                    };
                }
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Kunne ikke sette inn denne brukeren i databasen.")
            };
        }

        /* GET api/Login/LogIn/{userName}
        [HttpGet, Route("LogIn/{userName}")]
        public HttpResponseMessage LogIn(String userName)
        {
            User user = dbWishList.GetUserByUserName(userName);

            if (user == null)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Kunne ikke finne denne brukeren i databasen.")
                };
            }

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(user);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }*/

        /* GET api/Login/noe/noeannet
        [Route("{userName}/{password}")]
        public HttpResponseMessage Get(String userName, String password)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.userName = userName;
            loginUser.password = password;

            User user = dbWishList.GetUserByLogin(loginUser);

            if (user == null)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Kunne ikke finne denne brukeren i databasen.")
                };
            }

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(user);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
        */
        /* GET
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Du har ikke tilgang.")
            };
        }*/
        /*
        // GET
        public HttpResponseMessage Get(int id)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Du har ikke tilgang.")
            };
        }*/

        /* POST api/Login
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
        }*/
        /*
        // PUT
        public HttpResponseMessage Put(int id, [FromBody]LoginUser loginUser)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Du har ikke tilgang.")
            };
        }*/
        /*
        // DELETE
        public HttpResponseMessage Delete(int id)
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Du har ikke tilgang.")
            };
        }*/
    }
}