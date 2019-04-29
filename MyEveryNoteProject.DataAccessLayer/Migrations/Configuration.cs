namespace MyEveryNoteProject.DataAccessLayer.Migrations
{
    using MyEveryNoteProject.Common.Helpers;
    using MyEveryNoteProject.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
   
    internal sealed class Configuration : DbMigrationsConfiguration<MyEveryNoteProject.DataAccessLayer.EntityFramework.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MyEveryNoteProject.DataAccessLayer.EntityFramework.DatabaseContext";
        }

        protected override void Seed(MyEveryNoteProject.DataAccessLayer.EntityFramework.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Adding admin user...
            //Adding admin user...
            EveryNoteUser admin = new EveryNoteUser()
            {
                Id=1,
                Name = "alican",
                Surname = "yilmaz",
                Email = "alicanyilmaz101@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "03alican",
                ProfileImageFileName = "defaultuser.png",
                Password = CryptoHelper.Manager("Alican?13"),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifierUser = "03alican"
            };

            //Adding standart user...
            EveryNoteUser standartuser = new EveryNoteUser()
            {   
                Id=2,
                Name = "kemal",
                Surname = "tanca",
                Email = "tancakemal@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "tancakemal",
                ProfileImageFileName = "defaultuser.png",
                Password = CryptoHelper.Manager("Tanca?13"),
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(45),
                ModifierUser = "tancakemal"
            };

            context.Dbset_EveryNoteUsers.AddOrUpdate(admin);
            context.Dbset_EveryNoteUsers.AddOrUpdate(standartuser);

            //Adding fake other user
            for (int c = 0; c < 8; c++)
            {
                EveryNoteUser user = new EveryNoteUser()
                {
                    Id=c+3,
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ProfileImageFileName = "defaultuser.png",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{c}",  //New String Formatting özelliði =$"user{c.ToString()}" þeklinde metod dahi kullanabilirsin.
                    Password = CryptoHelper.Manager($"user?131313"),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifierUser = $"user{c}"
                };

                context.Dbset_EveryNoteUsers.AddOrUpdate(user);
            }

            context.SaveChanges();
            //user list for using for every tables inside for loop
            List<EveryNoteUser> user_list = context.Dbset_EveryNoteUsers.ToList();

            //Adding Fake categories
            for (int i = 0; i < 10; i++)
            {
                Category category_add = new Category()
                {
                    Id=i+1,
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifierUser = "03alican"
                };

                context.Dbset_Categories.AddOrUpdate(category_add);

                //Adding Fake Articles inside this category
                int counter = 1;
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 10); k++)
                {
                    EveryNoteUser article_owner = user_list[FakeData.NumberData.GetNumber(0, user_list.Count - 1)];

                    Article article_add = new Article()
                    {
                        
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        //Categories = category_add, //Bunu yazmayadabilirsin cunku zaten o category nin içerisindeki notlarý o notu ekliyorsunuz. Category si belli zaten.
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
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 6); j++)
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
                    for (int m = 0; m < article_add.LikeCount; m++)
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
