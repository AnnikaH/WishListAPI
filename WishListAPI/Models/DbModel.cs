using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Core.EntityClient;
using System.Data.Common;

namespace WishListAPI.Models
{
    public class WishTips
    {
        public int ID { get; set; }
        public int SenderId { get; set; }   // foreign key from User
        public int ReceiverId { get; set; } // foreign key from User
        public String Name { get; set; }
        public String Spesification { get; set; }
        public byte[] Image { get; set; }
        public double Price { get; set; }
        public String Where { get; set; }
        public String Link { get; set; }

        public virtual Users Sender { get; set; }
        public virtual Users Receiver { get; set; }
    }

    public class Users
    {
        public int ID { get; set; }
        public String UserName { get; set; }
        public byte[] Password { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }

        public virtual List<WishLists> WishLists { get; set; }
        public virtual List<Sharings> Sharings { get; set; }
        public virtual List<WishTips> WishTips { get; set; } // both received and sent (split into two?)
    }

    public class Wishes
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Spesification { get; set; }
        public byte[] Image { get; set; }
        public double Price { get; set; }
        public String Where { get; set; }
        public String Link { get; set; }
        public int WishListId { get; set; } // foreign key from WishList

        public virtual WishLists WishList { get; set; }
    }

    public class WishLists
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int OwnerId { get; set; }    // foreign key from User
        
        public virtual Users Owner { get; set; }
        
        public virtual List<Sharings> Sharings { get; set; }
        public virtual List<Wishes> Wishes { get; set; }
    }

    public class Sharings
    {
        public int ID { get; set; }
        public int UserId { get; set; }     // foreign key from User
        public int WishListId { get; set; } // foreign key from WishList

        public virtual Users User { get; set; }
        public virtual WishLists WishList { get; set; }
    }

    public class WishListContext : DbContext
    {
        public WishListContext()
          : base("name=WishList")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<WishTips> WishTips { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Wishes> Wishes { get; set; }
        public DbSet<WishLists> WishLists { get; set; }
        public DbSet<Sharings> Sharings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<WishLists>()
                .HasRequired(w => w.Owner)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WishTips>()
                .HasRequired(w => w.Sender)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}