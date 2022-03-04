using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.MembershipTypes.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }

                context.MembershipTypes.AddRange(
                    new MembershipType
                    {
                        Id = 1,
                        Name = "Pay as You Go",
                        SignUpFee = 0,
                        DurationInMonths = 0,
                        DiscountRate = 0
                    },
                    new MembershipType
                    {
                        Id = 2,
                        Name = "Monthly",
                        SignUpFee = 30,
                        DurationInMonths = 1,
                        DiscountRate = 10
                    },
                    new MembershipType
                    {
                        Id = 3,
                        Name = "Quaterly",
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
                    new MembershipType
                    {
                        Id = 4,
                        Name = "Yearly",
                        SignUpFee = 300,
                        DurationInMonths = 12,
                        DiscountRate = 20
                    });

                context.Books.Add(new Book()
                {
                    AuthorName = "John Smith",
                    DateAdded = DateTime.Now,
                    Name = "My new awsome book!",
                    NumberAvailable = 100,
                    NumberInStock = 200,
                    ReleaseDate = DateTime.Now,
                    GenreId = 1,
                });

                context.Books.Add(new Book()
                {
                    AuthorName = "John Smith",
                    DateAdded = DateTime.Now,
                    Name = "My new awsome book 2!",
                    NumberAvailable = 100,
                    NumberInStock = 200,
                    ReleaseDate = DateTime.Now,
                    GenreId = 1,
                });

                context.Customers.AddRange(new Customer()
                {
                    Name = "Angeline Jole",
                    Birthdate = DateTime.Now.AddYears(-24),
                    HasNewsletterSubscribed = false,
                    MembershipTypeId = 1
                }, new Customer()
                {
                    Name = "Frank Jole",
                    Birthdate = DateTime.Now.AddYears(-24),
                    HasNewsletterSubscribed = false,
                    MembershipTypeId = 1
                });

                context.SaveChanges();
            }
        }
    }
}