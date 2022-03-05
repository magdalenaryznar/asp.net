using System;
using System.Linq;
using LibApp.Data;
using Microsoft.AspNetCore.Identity;
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


                context.Genre.AddRange(
                    new Genre { Name = "Sci-fi" },
                    new Genre()
                    {
                        Name = "Other"
                    });


                context.SaveChanges();
            }
        }

        public static void SeedRolesAndUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {

            
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                roleManager.CreateAsync(new Role() { Name = "User" }).Wait();
            }

            if (!roleManager.RoleExistsAsync("Owner").Result)
            {
                roleManager.CreateAsync(new Role() { Name = "Owner" }).Wait();
            }

            if (!roleManager.RoleExistsAsync("StoreManager").Result)
            {
                roleManager.CreateAsync(new Role() { Name = "StoreManager" }).Wait();
            }

            if (userManager.FindByEmailAsync("johndoe@test.com").Result == null)
            {
                User user = new User();
                user.UserName = "user@test.com";
                user.Email = "user@test.com";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }


            if (userManager.FindByEmailAsync("alex@test.com").Result == null)
            {
                User user = new User();
                user.UserName = "storemanager@test.com";
                user.Email = "storemanager@test.com";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "StoreManager").Wait();
                }
            }

            if (userManager.FindByEmailAsync("alex@test.com").Result == null)
            {
                User user = new User();
                user.UserName = "owner@test.com";
                user.Email = "owner@test.com";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Owner").Wait();
                }
            }
        }
    }


}