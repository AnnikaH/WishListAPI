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
    public class UserController : ApiController
    {
        DBWishList dbWishList = new DBWishList();

        /* GET api/User
        // [HttpGet, Route("{userName}/{password}")]
        public bool LogIn(String userName, String password)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.userName = userName;
            loginUser.password = password;

            bool ok = dbWishList.UserInDb(loginUser);

            return ok;
        }*/

        // Fungerer GET api/User/Login/noe/noe
        [HttpGet, Route("LogIn/{userName}/{password}")]
        public bool LogIn(String userName, String password)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.userName = userName;
            loginUser.password = password;

            bool ok = dbWishList.UserInDb(loginUser);

            return ok;
        }

        // GET api/User
        public HttpResponseMessage Get()
        {
            List<User> allUsers = dbWishList.GetAllUsers();

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allUsers);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
        
        // GET api/User/Get/5
        public HttpResponseMessage Get(int id)
        {
            User oneUser = dbWishList.GetUser(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(oneUser);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        // POST api/User
        public HttpResponseMessage Post(User user)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.CreateUser(user);

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
                Content = new StringContent("Kunne ikke sette inn denne brukeren i databasen.")
            };
        }

        // PUT api/User/5
        public HttpResponseMessage Put(int id, [FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.UpdateUser(id, user);

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
                Content = new StringContent("Kunne ikke endre bruker med id " + id + " i databasen.")
            };
        }

        // DELETE api/User/5
        public HttpResponseMessage Delete(int id)
        {
            bool ok = dbWishList.DeleteUser(id);

            if (!ok)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Kunne ikke slette bruker med id " + id + " fra databasen.")
                };
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}