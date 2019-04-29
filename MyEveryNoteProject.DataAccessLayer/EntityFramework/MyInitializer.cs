using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEveryNoteProject.Entities;

namespace MyEveryNoteProject.DataAccessLayer.EntityFramework
{
    public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        
        protected override void Seed(DatabaseContext context)
        {

            //Adding admin user...
            EveryNoteUser admin = new EveryNoteUser()
            {
                Name = "alican",
                Surname = "yilmaz",
                Email = "alicanyilmaz101@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "03alican",
                ProfileImageFileName = "defaultuser.png",
                Password = "Alican?13",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifierUser = "03alican"
            };

            //Adding standart user...
            EveryNoteUser standartuser = new EveryNoteUser()
            {
                Name = "kemal",
                Surname = "tanca",
                Email = "tancakemal@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "tancakemal",
                ProfileImageFileName = "defaultuser.png",
                Password = "Tanca?13",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(45),
                ModifierUser = "tancakemal"
            };

            context.Dbset_EveryNoteUsers.Add(admin);
            context.Dbset_EveryNoteUsers.Add(standartuser);

            //Adding fake other user
            for (int c = 0; c < 8; c++)
            {
                EveryNoteUser user = new EveryNoteUser()
                {
                    Name =FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email =FakeData.NetworkData.GetEmail(),
                    ProfileImageFileName = "defaultuser.png",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username =$"user{c}",  //New String Formatting özelliği =$"user{c.ToString()}" şeklinde metod dahi kullanabilirsin.
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifierUser = $"user{c}"
                };

                context.Dbset_EveryNoteUsers.Add(user);
            }

            context.SaveChanges();
            //user list for using for every tables inside for loop
            List<EveryNoteUser> user_list = context.Dbset_EveryNoteUsers.ToList();

            //Adding Fake categories
            for (int i = 0; i < 10; i++)
            {
                Category category_add = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifierUser = "03alican"
                };

                context.Dbset_Categories.Add(category_add);

                //Adding Fake Articles inside this category
                int counter = 1;
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,10); k++)
                {
                    EveryNoteUser article_owner = user_list[FakeData.NumberData.GetNumber(0, user_list.Count - 1)];

                    Article article_add = new Article()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        //Categories = category_add, //Bunu yazmayadabilirsin cunku zaten o category nin içerisindeki notları o notu ekliyorsunuz. Category si belli zaten.
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = article_owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifierUser = article_owner.Username,
                        ArticleImageFileName = $"image_{counter}.jpg",
                    };

                    category_add.Articles.Add(article_add);

                    if (counter >= 11)
                    {
                        counter = 1;
                    }
                    else
                    {
                        counter++;
                    }
                       

                    //Adding Fake Comments inside this Article
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,6); j++)
                    {
                        EveryNoteUser comment_owner = user_list[FakeData.NumberData.GetNumber(0, user_list.Count - 1)];

                        Comment comment_add = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            //Articles = article_add, //Bunu yazmayadabilirsin 
                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifierUser = comment_owner.Username
                        };

                        article_add.Comments.Add(comment_add);
                    }

                    //Adding Fake Likes inside this Article
                    for (int m = 0; m <article_add.LikeCount; m++)
                    {
                        Liked likes_add = new Liked()
                        {
                            LikedUser = user_list[m]
                        };

                        article_add.Likes.Add(likes_add); 
                    }

                }

            }

            context.SaveChanges();

        }
    }
}
