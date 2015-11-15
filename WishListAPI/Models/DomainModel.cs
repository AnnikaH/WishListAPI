using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WishListAPI.Models
{
    public class WishTip
    {
        public int id { get; set; }
        public int senderId { get; set; }   // foreign key from User
        public int receiverId { get; set; } // foreign key from User
        public String name { get; set; }
        public String spesification { get; set; }
        public byte[] image { get; set; }
        public double price { get; set; }
        public String where { get; set; }
        public String link { get; set; }
    }

    public class LoginUser
    {
        public String userName { get; set; }
        public String password { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public String userName { get; set; }
        public String password { get; set; }
        public String email { get; set; }
        public String phoneNumber { get; set; }
    }

    public class Wish
    {
        public int id { get; set; }
        public String name { get; set; }
        public String spesification { get; set; }
        public byte[] image { get; set; }
        public double price { get; set; }
        public String where { get; set; }
        public String link { get; set; }
        public int wishListId { get; set; } // foreign key from WishList
    }

    public class WishList
    {
        public int id { get; set; }
        public String name { get; set; }
        public int ownerId { get; set; }    // foreign key from User
    }

    public class Sharing
    {
        public int id { get; set; }
        public int userId { get; set; }     // foreign key from User
        public int wishListId { get; set; } // foreign key from WishList
    }
}