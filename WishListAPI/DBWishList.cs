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

        // Create (husk virtual entall)
        // Update (husk virtual entall)
        // Delete

        // -------------------------- WishTip -----------------------------



        /*
        // -------------------------- Category ----------------------------

        public List<Category> GetAllCategories()
        {
            try
            {
                List<Category> allCategories = db.Categories.Select(c => new Category()
                {
                    id = c.ID,
                    name = c.Name
                }).ToList();

                return allCategories;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<Category> allCategories = new List<Category>();
                return allCategories;
            }
        }

        public Category GetCategory(int id)
        {
            try
            {
                Categories oneDbCategory = db.Categories.Find(id);

                if (oneDbCategory == null)
                    return null;

                var oneCategory = new Category()
                {
                    id = oneDbCategory.ID,
                    name = oneDbCategory.Name
                };

                return oneCategory;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public bool CreateCategory(Category category)
        {
            var newCategory = new Categories
            {
                Name = category.name
            };

            try
            {
                // save category
                db.Categories.Add(newCategory);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool UpdateCategory(int id, Category category)
        {
            Categories foundCategory = db.Categories.FirstOrDefault(c => c.ID == id);

            if (foundCategory == null)
                return false;

            foundCategory.Name = category.name;

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

        public bool DeleteCategory(int id)
        {
            try
            {
                Categories foundCategory = db.Categories.Find(id);

                if (foundCategory == null)
                    return false;

                db.Categories.Remove(foundCategory);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        // ----------------------------- FAQ ------------------------------

        public List<FAQ> GetAllFAQs()
        {
            try
            {
                List<FAQ> allFAQs = db.FAQs.Select(f => new FAQ()
                {
                    id = f.ID,
                    question = f.Question,
                    answer = f.Answer,
                    categoryId = f.CategoryId
                }).ToList();

                return allFAQs;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<FAQ> allFAQs = new List<FAQ>();
                return allFAQs;
            }
        }

        public FAQ GetFAQ(int id)
        {
            try
            {
                FAQs oneDbFAQ = db.FAQs.Find(id);

                if (oneDbFAQ == null)
                    return null;

                var oneFAQ = new FAQ()
                {
                    id = oneDbFAQ.ID,
                    question = oneDbFAQ.Question,
                    answer = oneDbFAQ.Answer,
                    categoryId = oneDbFAQ.CategoryId
                };

                return oneFAQ;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public List<FAQ> GetFAQsFromCategory(int categoryId)
        {
            try
            {
                var dbFAQs = db.FAQs.ToList();
                List<FAQ> FAQs = new List<FAQ>();

                foreach (var faq in dbFAQs)
                {
                    if (faq.CategoryId == categoryId)
                    {
                        var oneFAQ = new FAQ();
                        oneFAQ.id = faq.ID;
                        oneFAQ.question = faq.Question;
                        oneFAQ.answer = faq.Answer;
                        oneFAQ.categoryId = faq.CategoryId;

                        FAQs.Add(oneFAQ);
                    }
                }

                return FAQs;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<FAQ> FAQs = new List<FAQ>();
                return FAQs;
            }
        }

        public bool CreateFAQ(FAQ faq)
        {
            var newFAQ = new FAQs
            {
                Question = faq.question,
                Answer = faq.answer,
                CategoryId = faq.categoryId
            };

            Categories foundCategory = db.Categories.Find(faq.categoryId);

            if (foundCategory != null)
                newFAQ.Category = foundCategory;
            else
                return false;

            try
            {
                // save FAQ
                db.FAQs.Add(newFAQ);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool UpdateFAQ(int id, FAQ faq)
        {
            FAQs foundFAQ = db.FAQs.FirstOrDefault(f => f.ID == id);

            if (foundFAQ == null)
                return false;

            foundFAQ.Question = faq.question;
            foundFAQ.Answer = faq.answer;
            foundFAQ.CategoryId = faq.categoryId;
            foundFAQ.Category = db.Categories.Find(faq.categoryId);

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

        public bool DeleteFAQ(int id)
        {
            try
            {
                FAQs foundFAQ = db.FAQs.Find(id);

                if (foundFAQ == null)
                    return false;

                db.FAQs.Remove(foundFAQ);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        // ---------------------------- Request --------------------------

        public List<Request> GetAllRequests()
        {
            try
            {
                List<Request> allRequests = db.Requests.Select(r => new Request()
                {
                    id = r.ID,
                    senderFirstName = r.SenderFirstName,
                    senderLastName = r.SenderLastName,
                    senderEmail = r.SenderEmail,
                    subject = r.Subject,
                    question = r.Question,
                    date = r.Date,
                    answered = r.Answered
                }).ToList();

                return allRequests;
            }
            catch (Exception e)
            {
                writeToLog(e);
                List<Request> allRequests = new List<Request>();
                return allRequests;
            }
        }

        public Request GetRequest(int id)
        {
            try
            {
                Requests oneDbRequest = db.Requests.Find(id);

                if (oneDbRequest == null)
                    return null;

                var oneRequest = new Request()
                {
                    id = oneDbRequest.ID,
                    senderFirstName = oneDbRequest.SenderFirstName,
                    senderLastName = oneDbRequest.SenderLastName,
                    senderEmail = oneDbRequest.SenderEmail,
                    subject = oneDbRequest.Subject,
                    question = oneDbRequest.Question,
                    date = oneDbRequest.Date,
                    answered = oneDbRequest.Answered
                };

                return oneRequest;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return null;
            }
        }

        public bool CreateRequest(Request request)
        {
            var newRequest = new Requests
            {
                SenderFirstName = request.senderFirstName,
                SenderLastName = request.senderLastName,
                SenderEmail = request.senderEmail,
                Subject = request.subject,
                Question = request.question,
                Date = request.date,
                Answered = request.answered
            };

            try
            {
                // save request
                db.Requests.Add(newRequest);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }

        public bool UpdateRequest(int id, Request request)
        {
            Requests foundRequest = db.Requests.FirstOrDefault(r => r.ID == id);

            if (foundRequest == null)
                return false;

            foundRequest.SenderFirstName = request.senderFirstName;
            foundRequest.SenderLastName = request.senderLastName;
            foundRequest.SenderEmail = request.senderEmail;
            foundRequest.Subject = request.subject;
            foundRequest.Question = request.question;
            foundRequest.Date = request.date;
            foundRequest.Answered = request.answered;

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

        public bool DeleteRequest(int id)
        {
            try
            {
                Requests foundRequest = db.Requests.Find(id);

                if (foundRequest == null)
                    return false;

                db.Requests.Remove(foundRequest);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }

            return true;
        }*/
    }
}