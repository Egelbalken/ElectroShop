﻿using ElectroShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Data
{
    public static class ContextSeed
    {
        private static Random random = new Random(42);

        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Customer.ToString()));
        }
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "SuperBardia",
                Email = "SuperBardia@gmail.com",
                FirstName = "Super",
                LastName = "Duper",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Super123#");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Customer.ToString());
                }

            }
        }

        public static async Task SeedCustomerAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "CustomerBardia",
                Email = "CustomerBardia@gmail.com",
                FirstName = "Customer",
                LastName = "IsKing",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Customer123#");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Customer.ToString());
                }

            }
        }

        public static async Task SeedCategoriesAsync(ApplicationDbContext context)
        {
            if (context.Categories.Count() != 0)
                return;

            using (var reader = new StreamReader("Data/Seed/categories.json"))
            {
                var categories = await JsonSerializer.DeserializeAsync<List<CategoryModel>>(reader.BaseStream);
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedProductsAsync(ApplicationDbContext context)
        {
            if (context.Products.Count() != 0)
                return;

            var allSubcategories = await context.Categories
                .Include(category => category.ParentCategory)
                .Where(category => category.ParentCategory != null)
                .ToListAsync();

            foreach (var category in allSubcategories)
            {
                int productsPerCategory = 10;
                for (int i = 0; i < productsPerCategory; i++)
                {
                    string garbageValue = new string(Enumerable
                        .Range(1, random.Next(2, 10))
                        .Select(_ => (char)random.Next('A', 'Z' + 1))
                        .ToArray());

                    var product = new ProductModel
                    {
                        CategoryId = category.CategoryId,
                        Name = $"{category.Name} {garbageValue}",
                        Description = "",
                        Price = random.Next(5, 100),
                        ImageURL = category.ImageURL
                    };

                    await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task SeedCategoriesAndProductsAsync(ApplicationDbContext context)
        {
            var parentCategories = new List<CategoryModel>
            {
               new CategoryModel
               {
                   Name = "Batterier",
                   Description = "Alla typer av batterier.",
                   ImageURL = "",
               },
               new CategoryModel
               {
                   Name = "Dioder",
                   Description = "Alla typer av dioder.",
                   ImageURL = ""
               }
            };

            var subCategories = new List<CategoryModel>
            {
                new CategoryModel
                {
                    Name = "Alkaliska",
                    Description = "Icke-uppladdningsbara alkaliska batterier",
                    ImageURL = "",
                    ParentCategory = parentCategories[0]
                },
                new CategoryModel
                {
                    Name = "Litium",
                    Description = "Icke-uppladdningsbara litiumbatterier och knappcellsbatterier.",
                    ImageURL = "",
                    ParentCategory = parentCategories[0]
                },
                new CategoryModel
                {
                    Name = "Zink-Kol",
                    Description = "Alla typer av Zink-Kol batterier.",
                    ImageURL = "",
                    ParentCategory = parentCategories[0]
                }
            };

            var products = new List<ProductModel>
            {
                new ProductModel
                {
                    Name = "Batteri 12V LR23",
                    Description = "Alkaliskt batteri på 12 volt.",
                    Price = 42,
                    ImageURL = "",
                    Category = subCategories[0]
                },
                new ProductModel
                {
                    Name = "Batteri Litium 3.6V 1/2 AA",
                    Description = "Litium batteri på 3.6 volt.",
                    Price = 34,
                    ImageURL = "",
                    Category = subCategories[1]
                },
                new ProductModel
                {
                    Name = "CR1220 batteri lithium 3V",
                    Description = "Litium batteri på 3 volt.",
                    Price = 32,
                    ImageURL = "",
                    Category = subCategories[1]
                }
            };

            if (context.Categories.Count() == 0)
            {
                await context.AddRangeAsync(parentCategories);
                await context.AddRangeAsync(subCategories);
                await context.SaveChangesAsync();
            }

            if (context.Products.Count() == 0)
            {
                await context.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedProductReviewsAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.ProductReviews.Count() == 0)
            {
                var customer = await userManager.FindByEmailAsync("CustomerBardia@gmail.com");
                var productReviews = context.Products
                    .Select(product => new ProductReviewModel
                    {
                        Title = $"I recommend {product.Name}!",
                        Review = $"{product.Name} worked very well. I followed the instructions step by step and it turned out great.",
                        Customer = customer,
                        ProductId = product.ProductId,
                        Rating = new ProductRatingModel 
                        { 
                            Rating = random.Next(1, 6), 
                            Customer = customer, 
                            Product = product 
                        }
                    });

                await context.ProductReviews.AddRangeAsync(productReviews);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedProductRatingsAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.ProductRatings.Count() == 0)
            {
                var customer = await userManager.FindByEmailAsync("CustomerBardia@gmail.com");
                var productRatings = context.Products
                    .Select(product => new ProductRatingModel
                    {
                        Rating = random.Next(1, 6),
                        Customer = customer,
                        ProductId = product.ProductId
                    });

                await context.ProductRatings.AddRangeAsync(productRatings);
                await context.SaveChangesAsync();
            }
        }
    }
}
