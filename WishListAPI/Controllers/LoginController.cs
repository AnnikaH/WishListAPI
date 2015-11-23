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
    }
}