using ElectroShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Data
{
    public static class ContextSeed
    {
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
    }
}
