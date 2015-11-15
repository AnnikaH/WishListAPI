using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WishListAPI.Controllers
{
    public class WishListController : ApiController
    {
        

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
    }
}