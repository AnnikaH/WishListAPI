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

        /* Fungerer GET api/User/Login/noe/noe
        [HttpGet, Route("LogIn/{userName}/{password}")]
        public bool LogIn(String userName, String password)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.userName = userName;
            loginUser.password = password;

            bool ok = dbWishList.UserInDb(loginUser);

            return ok;
        }*/

        /* GET api/User
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
        }*/

        /* GET api/User/noe/noeannet
        [Route("{userName}/{password}")]
        public HttpResponseMessage Get(String userName, String password)
        {
            LoginUser loginUser = new LoginUser();
            loginUser.userName = userName;
            loginUser.password = password;

            User user = dbWishList.GetUserByLogin(loginUser);

            if(user == null)
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

        /*
        // GET api/User/loginUser
        public HttpResponseMessage Get(LoginUser loginUser)
        {
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
        }*/

        /* GET api/User/{userName}
        public HttpResponseMessage Get(String userName)
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

        // GET api/User/5
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