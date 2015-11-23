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

/* Tor sitt WebAPI til apputvikling (kun dette):
public class Kunde
{
    public string Navn { get; set; }
    public string Adresse { get; set; }
    public string Telefonnr { get; set; }
}

public List<Kunde> Lagliste()
{
    var Kunde1 = new Kunde
    {
        Navn = "Per Hansen",
        Adresse = "Osloveien 82",
        Telefonnr = "12345678"
    };
    var Kunde2 = new Kunde
    {
        Navn = "Ole Olsen",
        Adresse = "Askerveien 43",
        Telefonnr = "87654321"
    };
    var Kunder = new List<Kunde>();
    Kunder.Add(Kunde1);
    Kunder.Add(Kunde2);
    return Kunder;
}
        
// GET api/WishList/Get
public List<Kunde> Get()
{
    List<Kunde> listeAvKunder = Lagliste();
    return listeAvKunder;
}
        
// GET api/WishList/Get/1
public Kunde Get(int id)
{
    List<Kunde> listeAvKunder = Lagliste();
    return listeAvKunder[id];
}

// POST api/WishList/Post
[HttpPost]
public List<Kunde> Post(Kunde kundeInn)
{
    List<Kunde> listeAvKunder = Lagliste();
    listeAvKunder.Add(kundeInn);
    return listeAvKunder;
}*/

namespace WishListAPI.Controllers
{
    public class WishListController : ApiController
    {
        DBWishList dbWishList = new DBWishList();

        /*
        // GET api/WishList/GetAllWishLists
        public HttpResponseMessage GetAllWishLists()
        {
            List<WishList> allWishLists = dbWishList.GetAllWishLists();

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allWishLists);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
        */

        // GET api/WishList/GetAllWishListsForUser/5
        public HttpResponseMessage GetAllWishListsForUser(int id)
        {
            List<WishList> allWishListsForUser = dbWishList.GetAllWishListsForUser(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allWishListsForUser);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        /* GET api/WishList/5
        public HttpResponseMessage Get(int id)
        {
            WishList oneWishList = dbWishList.GetWishList(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(oneWishList);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }*/

        // POST api/WishList
        public HttpResponseMessage Post(WishList wishList)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.CreateWishList(wishList);

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
                Content = new StringContent("Kunne ikke sette inn denne ønskelisten i databasen.")
            };
        }

        // PUT api/WishList/5
        public HttpResponseMessage Put(int id, [FromBody]WishList wishList)
        {
            if (ModelState.IsValid)
            {
                bool ok = dbWishList.UpdateWishList(id, wishList);

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
                Content = new StringContent("Kunne ikke endre ønskeliste med id " + id + " i databasen.")
            };
        }

        // DELETE api/WishList/5
        public HttpResponseMessage Delete(int id)
        {
            bool ok = dbWishList.DeleteWishList(id);

            if (!ok)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Kunne ikke slette ønskeliste med id " + id + " fra databasen.")
                };
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}