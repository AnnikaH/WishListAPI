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

        [Required]
        [RegularExpression("^[0-9]{1,10}$")]
        public int senderId { get; set; }   // foreign key from User

        [Required]
        [RegularExpression("^[0-9]{1,10}$")]
        public int receiverId { get; set; } // foreign key from User

        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ0-9., \\-]{2,30}$")]
        public String name { get; set; }

        public String spesification { get; set; }

        public byte[] image { get; set; }

        [RegularExpression("^[0-9\\.]{0,9}$")]
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

        [Required]
        [RegularExpression("[A-ZÆØÅa-zæøå0-9_\\-]{1,30}")]
        public String userName { get; set; }

        [Required]
        [RegularExpression("[A-ZÆØÅa-zæøå0-9!#$%&'*+\\-/=?\\^_`{|}~+(\\.]{8,30}")]
        public String password { get; set; }

        [Required]
        [RegularExpression("^[-a-z0-9~!$%^&*_=+}{\'?]+(\\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\\.[-a-z0-9_]+)*\\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}))(:[0-9]{1,5})?$")]
        public String email { get; set; }

        [Required]
        [RegularExpression("[0-9]{8}")]
        public String phoneNumber { get; set; }
    }

    public class Wish
    {
        public int id { get; set; }

        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ0-9., \\-]{2,30}$")]
        public String name { get; set; }
        
        public String spesification { get; set; }

        public byte[] image { get; set; }

        [RegularExpression("^[0-9\\.]{0,9}$")]
        public double price { get; set; }

        public String where { get; set; }

        public String link { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,10}$")]
        public int wishListId { get; set; } // foreign key from WishList
    }

    public class WishList
    {
        public int id { get; set; }

        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ0-9., \\-]{2,30}$")]
        public String name { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,10}$")]
        public int ownerId { get; set; }    // foreign key from User
    }

    public class Sharing
    {
        public int id { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,10}$")]
        public int userId { get; set; }     // foreign key from User

        [Required]
        [RegularExpression("^[0-9]{1,10}$")]
        public int wishListId { get; set; } // foreign key from WishList
    }
}