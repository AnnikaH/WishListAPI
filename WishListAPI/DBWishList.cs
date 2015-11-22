using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using WishListAPI.Models;
using System.IO;

namespace WishListAPI
{
    public class DBWishList
    {
        WishListContext db = new WishListContext();
        String errorLogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs";

        // ---------------------------- Log -------------------------------

        public void writeToLog(Exception e)
        {
            String errorMessage = e.Message.ToString() + " in " + e.TargetSite.ToString() + e.StackTrace.ToString();

            String day = DateTime.Now.Day.ToString();
            String month = DateTime.Now.Month.ToString();
            String year = DateTime.Now.Year.ToString();
            String today = "" + day + "." + month + "." + year;
            String nowHour = DateTime.Now.Hour.ToString();
            String nowMinute = DateTime.Now.Minute.ToString();
            String todayFile = @"\Log " + today + ".txt";

            if (File.Exists(errorLogPath + todayFile))
            {
                using (StreamWriter outputFile = new StreamWriter("" + errorLogPath + todayFile, true))
                {
                    outputFile.WriteLine("[" + nowHour + ":" + nowMinute + "] " + errorMessage);
                }
            }
            else
            {
                if (!Directory.Exists(errorLogPath))
                {
                    Directory.CreateDirectory(errorLogPath);
                }
                using (StreamWriter outputFile = new StreamWriter("" + errorLogPath + todayFile))
                {
                    outputFile.WriteLine("[" + nowHour + ":" + nowMinute + "] " + errorMessage);
                }
            }
        }

        // ------------------------- User and LoginUser --------------------------

        public List<User> GetAllUsers()
        {
            try
            {
                List<User> allUsers = db.Users.Select(u => new User()
                {
                    id = u.ID,
                    userName = u.UserName,
                    password = null,
                    email = u.Email,
                    phoneNumber = u.PhoneNumber
                }).ToList();

                return allUsers;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<User> allUsers = new List<User>();
                return allUsers;
            }
        }

        public User GetUser(int id)
        {
            try
            {
                Users oneDbUser = db.Users.Find(id);

                if (oneDbUser == null)
                    return null;

                var oneUser = new User()
                {
                    id = oneDbUser.ID,
                    userName = oneDbUser.UserName,
                    password = null,
                    email = oneDbUser.Email,
                    phoneNumber = oneDbUser.PhoneNumber
                };

                return oneUser;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public User GetUserByUserName(String userName)
        {
            try
            {
                Users foundUser = db.Users.FirstOrDefault(
                    u => u.UserName == userName);

                if (foundUser == null)
                    return null;

                var oneUser = new User()
                {
                    id = foundUser.ID,
                    userName = foundUser.UserName,
                    password = null,
                    email = foundUser.Email,
                    phoneNumber = foundUser.PhoneNumber
                };

                return oneUser;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public User GetUserByLogin(LoginUser loginUser)
        {
            try
            {
                byte[] passwordDB = CreateHash(loginUser.password);

                Users foundUser = db.Users.FirstOrDefault(
                    u => u.Password == passwordDB && u.UserName == loginUser.userName);

                if (foundUser == null)
                    return null;

                var oneUser = new User()
                {
                    id = foundUser.ID,
                    userName = foundUser.UserName,
                    password = null,
                    email = foundUser.Email,
                    phoneNumber = foundUser.PhoneNumber
                };

                return oneUser;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public bool UserInDb(LoginUser loginUser)
        {
            try
            {
                byte[] passwordDB = CreateHash(loginUser.password);

                Users foundUser = db.Users.FirstOrDefault(
                    u => u.Password == passwordDB && u.UserName == loginUser.userName);

                if (foundUser == null)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }
        }

        private static byte[] CreateHash(String inPassword)
        {
            //Hash function to hash a password and return the hash
            byte[] input, output;
            var algorythm = System.Security.Cryptography.SHA256.Create();
            input = System.Text.Encoding.ASCII.GetBytes(inPassword);
            output = algorythm.ComputeHash(input);
            return output;
        }

        public bool CreateUser(User user)
        {
            // check if a user with the same username exists:

            List<Users> users = db.Users.ToList();
            
            foreach(var oneUser in users)
            {
                if(oneUser.UserName.Equals(user.userName))
                {
                    return false;
                }
            }

            var newUser = new Users
            {
                UserName = user.userName,
                Password = CreateHash(user.password),
                Email = user.email,
                PhoneNumber = user.phoneNumber
            };

            try
            {
                // save user
                db.Users.Add(newUser);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool UpdateUser(int id, User user)
        {
            Users foundUser = db.Users.FirstOrDefault(u => u.ID == id);

            if (foundUser == null)
                return false;

            foundUser.UserName = user.userName;
            foundUser.Password = CreateHash(user.password);
            foundUser.Email = user.email;
            foundUser.PhoneNumber = user.phoneNumber;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool DeleteUser(int id)
        {
            try
            {
                Users foundUser = db.Users.Find(id);

                if (foundUser == null)
                    return false;

                db.Users.Remove(foundUser);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        // ------------------------- WishList ----------------------------

        public List<WishList> GetAllWishLists()
        {
            try
            {
                List<WishList> allWishLists = db.WishLists.Select(w => new WishList()
                {
                    id = w.ID,
                    name = w.Name,
                    ownerId = w.OwnerId,
                }).ToList();

                return allWishLists;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<WishList> allWishLists = new List<WishList>();
                return allWishLists;
            }
        }

        public List<WishList> GetAllWishListsForUser(int ownerId)
        {
            try
            {
                List<WishLists> allDbWishLists = db.WishLists.ToList();
                List<WishList> outputWishLists = new List<WishList>();

                foreach (var wishListDb in allDbWishLists)
                {
                    if (wishListDb.OwnerId == ownerId)
                    {
                        WishList wishList = new WishList();
                        wishList.id = wishListDb.ID;
                        wishList.name = wishListDb.Name;
                        wishList.ownerId = wishListDb.OwnerId;

                        outputWishLists.Add(wishList);
                    }
                }

                return outputWishLists;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<WishList> outputWishLists = new List<WishList>();
                return outputWishLists;
            }
        }

        public WishList GetWishList(int id)
        {
            try
            {
                WishLists oneDbWishList = db.WishLists.Find(id);

                if (oneDbWishList == null)
                    return null;

                var oneWishList = new WishList()
                {
                    id = oneDbWishList.ID,
                    name = oneDbWishList.Name,
                    ownerId = oneDbWishList.OwnerId
                };

                return oneWishList;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public bool CreateWishList(WishList wishList)
        {
            var owner = db.Users.Find(wishList.ownerId);

            var newWishList = new WishLists
            {
                Name = wishList.name,
                OwnerId = wishList.ownerId,
                Owner = owner
            };

            try
            {
                // save wish list
                db.WishLists.Add(newWishList);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool UpdateWishList(int id, WishList wishList)
        {
            WishLists foundWishList = db.WishLists.FirstOrDefault(w => w.ID == id);

            if (foundWishList == null)
                return false;

            var owner = db.Users.Find(wishList.ownerId);

            foundWishList.Name = wishList.name;
            foundWishList.OwnerId = wishList.ownerId;
            foundWishList.Owner = owner;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool DeleteWishList(int id)
        {
            try
            {
                WishLists foundWishList = db.WishLists.Find(id);

                if (foundWishList == null)
                    return false;

                db.WishLists.Remove(foundWishList);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        // ---------------------------- Wish ------------------------------

        public List<Wish> GetAllWishes()
        {
            try
            {
                List<Wish> allWishes = db.Wishes.Select(w => new Wish()
                {
                    id = w.ID,
                    name = w.Name,
                    spesification = w.Spesification,
                    image = w.Image,
                    price = w.Price,
                    where = w.Where,
                    link = w.Link,
                    wishListId = w.WishListId
                }).ToList();

                return allWishes;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<Wish> allWishes = new List<Wish>();
                return allWishes;
            }
        }

        public List<Wish> GetAllWishesForList(int wishListId)
        {
            try
            {
                List<Wishes> allDbWishes = db.Wishes.ToList();
                List<Wish> outputWishes = new List<Wish>();

                foreach(var wishDb in allDbWishes)
                {
                    if (wishDb.WishListId == wishListId)
                    {
                        Wish wish = new Wish();
                        wish.id = wishDb.ID;
                        wish.name = wishDb.Name;
                        wish.spesification = wishDb.Spesification;
                        wish.image = wishDb.Image;
                        wish.price = wishDb.Price;
                        wish.where = wishDb.Where;
                        wish.link = wishDb.Link;
                        wish.wishListId = wishDb.WishListId;

                        outputWishes.Add(wish);
                    }
                }

                return outputWishes;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<Wish> outputWishes = new List<Wish>();
                return outputWishes;
            }
        }

        public Wish GetWish(int id)
        {
            try
            {
                Wishes oneDbWish = db.Wishes.Find(id);

                if (oneDbWish == null)
                    return null;

                var oneWish = new Wish()
                {
                    id = oneDbWish.ID,
                    name = oneDbWish.Name,
                    spesification = oneDbWish.Spesification,
                    image = oneDbWish.Image,
                    price = oneDbWish.Price,
                    where = oneDbWish.Where,
                    link = oneDbWish.Link,
                    wishListId = oneDbWish.WishListId
                };

                return oneWish;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public bool CreateWish(Wish wish)
        {
            var wishList = db.WishLists.Find(wish.wishListId);

            var newWish = new Wishes
            {
                Name = wish.name,
                Spesification = wish.spesification,
                Image = wish.image,
                Price = wish.price,
                Where = wish.where,
                Link = wish.link,
                WishListId = wish.wishListId,
                WishList = wishList
            };

            try
            {
                // save wish
                db.Wishes.Add(newWish);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool UpdateWish(int id, Wish wish)
        {
            Wishes foundWish = db.Wishes.FirstOrDefault(w => w.ID == id);

            if (foundWish == null)
                return false;

            var wishList = db.WishLists.Find(wish.wishListId);

            foundWish.Name = wish.name;
            foundWish.Spesification = wish.spesification;
            foundWish.Image = wish.image;
            foundWish.Price = wish.price;
            foundWish.Where = wish.where;
            foundWish.Link = wish.link;
            foundWish.WishListId = wish.wishListId;
            foundWish.WishList = wishList;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool DeleteWish(int id)
        {
            try
            {
                Wishes foundWish = db.Wishes.Find(id);

                if (foundWish == null)
                    return false;

                db.Wishes.Remove(foundWish);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        // -------------------------- Sharing -----------------------------
        
        public List<Sharing> GetAllSharings()
        {
            try
            {
                List<Sharing> allSharings = db.Sharings.Select(s => new Sharing()
                {
                    id = s.ID,
                    userId = s.UserId,
                    wishListId = s.WishListId
                }).ToList();

                return allSharings;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<Sharing> allSharings = new List<Sharing>();
                return allSharings;
            }
        }

        public Sharing GetSharing(int id)
        {
            try
            {
                Sharings oneDbSharing = db.Sharings.Find(id);

                if (oneDbSharing == null)
                    return null;

                var oneSharing = new Sharing()
                {
                    id = oneDbSharing.ID,
                    userId = oneDbSharing.UserId,
                    wishListId = oneDbSharing.WishListId
                };

                return oneSharing;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public bool CreateSharing(Sharing sharing)
        {
            var user = db.Users.Find(sharing.userId);
            var wishList = db.WishLists.Find(sharing.wishListId);

            var newSharing = new Sharings
            {
                UserId = sharing.userId,
                WishListId = sharing.wishListId,
                User = user,
                WishList = wishList
            };

            try
            {
                // save sharing
                db.Sharings.Add(newSharing);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool UpdateSharing(int id, Sharing sharing)
        {
            Sharings foundSharing = db.Sharings.FirstOrDefault(s => s.ID == id);

            if (foundSharing == null)
                return false;

            var user = db.Users.Find(sharing.userId);
            var wishList = db.WishLists.Find(sharing.wishListId);

            foundSharing.UserId = sharing.userId;
            foundSharing.WishListId = sharing.wishListId;
            foundSharing.User = user;
            foundSharing.WishList = wishList;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool DeleteSharing(int id)
        {
            try
            {
                Sharings foundSharing = db.Sharings.Find(id);

                if (foundSharing == null)
                    return false;

                db.Sharings.Remove(foundSharing);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        // -------------------------- WishTip -----------------------------

        public List<WishTip> GetAllWishTips()
        {
            try
            {
                List<WishTip> allWishTips = db.WishTips.Select(w => new WishTip()
                {
                    id = w.ID,
                    senderId = w.SenderId,
                    receiverId = w.ReceiverId,
                    name = w.Name,
                    spesification = w.Spesification,
                    image = w.Image,
                    price = w.Price,
                    where = w.Where,
                    link = w.Link
                }).ToList();

                return allWishTips;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<WishTip> allWishTips = new List<WishTip>();
                return allWishTips;
            }
        }

        public List<WishTip> GetAllWishTipsForSender(int senderId)
        {
            try
            {
                List<WishTips> allDbWishTips = db.WishTips.ToList();
                List<WishTip> outputWishTips = new List<WishTip>();

                foreach (var wishTipDb in allDbWishTips)
                {
                    if (wishTipDb.SenderId == senderId)
                    {
                        WishTip wishTip = new WishTip();
                        wishTip.id = wishTipDb.ID;
                        wishTip.senderId = wishTipDb.SenderId;
                        wishTip.receiverId = wishTipDb.ReceiverId;
                        wishTip.name = wishTipDb.Name;
                        wishTip.spesification = wishTipDb.Spesification;
                        wishTip.image = wishTipDb.Image;
                        wishTip.price = wishTipDb.Price;
                        wishTip.where = wishTipDb.Where;
                        wishTip.link = wishTipDb.Link;

                        outputWishTips.Add(wishTip);
                    }
                }

                return outputWishTips;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<WishTip> outputWishTips = new List<WishTip>();
                return outputWishTips;
            }
        }

        public List<WishTip> GetAllWishTipsForReceiver(int receiverId)
        {
            try
            {
                List<WishTips> allDbWishTips = db.WishTips.ToList();
                List<WishTip> outputWishTips = new List<WishTip>();

                foreach (var wishTipDb in allDbWishTips)
                {
                    if (wishTipDb.ReceiverId == receiverId)
                    {
                        WishTip wishTip = new WishTip();
                        wishTip.id = wishTipDb.ID;
                        wishTip.senderId = wishTipDb.SenderId;
                        wishTip.receiverId = wishTipDb.ReceiverId;
                        wishTip.name = wishTipDb.Name;
                        wishTip.spesification = wishTipDb.Spesification;
                        wishTip.image = wishTipDb.Image;
                        wishTip.price = wishTipDb.Price;
                        wishTip.where = wishTipDb.Where;
                        wishTip.link = wishTipDb.Link;

                        outputWishTips.Add(wishTip);
                    }
                }

                return outputWishTips;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<WishTip> outputWishTips = new List<WishTip>();
                return outputWishTips;
            }
        }

        public WishTip GetWishTip(int id)
        {
            try
            {
                WishTips oneDbWishTip = db.WishTips.Find(id);

                if (oneDbWishTip == null)
                    return null;

                var oneWishTip = new WishTip()
                {
                    id = oneDbWishTip.ID,
                    senderId = oneDbWishTip.SenderId,
                    receiverId = oneDbWishTip.ReceiverId,
                    name = oneDbWishTip.Name,
                    spesification = oneDbWishTip.Spesification,
                    image = oneDbWishTip.Image,
                    price = oneDbWishTip.Price,
                    where = oneDbWishTip.Where,
                    link = oneDbWishTip.Link
                };

                return oneWishTip;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public bool CreateWishTip(WishTip wishTip)
        {
            var sender = db.Users.Find(wishTip.senderId);
            var receiver = db.Users.Find(wishTip.receiverId);

            var newWishTip = new WishTips
            {
                SenderId = wishTip.senderId,
                ReceiverId = wishTip.receiverId,
                Name = wishTip.name,
                Spesification = wishTip.spesification,
                Image = wishTip.image,
                Price = wishTip.price,
                Where = wishTip.where,
                Link = wishTip.link,
                Sender = sender,
                Receiver = receiver
            };

            try
            {
                // save wish tip
                db.WishTips.Add(newWishTip);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool UpdateWishTip(int id, WishTip wishTip)
        {
            WishTips foundWishTip = db.WishTips.FirstOrDefault(w => w.ID == id);

            if (foundWishTip == null)
                return false;

            var sender = db.Users.Find(wishTip.senderId);
            var receiver = db.Users.Find(wishTip.receiverId);

            foundWishTip.SenderId = wishTip.senderId;
            foundWishTip.ReceiverId = wishTip.receiverId;
            foundWishTip.Name = wishTip.name;
            foundWishTip.Spesification = wishTip.spesification;
            foundWishTip.Image = wishTip.image;
            foundWishTip.Price = wishTip.price;
            foundWishTip.Where = wishTip.where;
            foundWishTip.Link = wishTip.link;
            foundWishTip.Sender = sender;
            foundWishTip.Receiver = receiver;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool DeleteWishTip(int id)
        {
            try
            {
                WishTips foundWishTip = db.WishTips.Find(id);

                if (foundWishTip == null)
                    return false;

                db.WishTips.Remove(foundWishTip);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }
    }
}