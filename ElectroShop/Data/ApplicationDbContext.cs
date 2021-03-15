using ElectroShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // DbSets to add to database.
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductReviewModel> ProductReviews { get; set; }
        public DbSet<ProductRatingModel> ProductRatings { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        // Constructor.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        /// <summary>
        /// Creates the Roles for Identity by start.
        /// </summary>
        /// <param name="builder">Creates roles from the databuilder in program class</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            // Create a hierarchical relationship between parent categories and child categories using a one to many relationship.
            // A parent category can have many subcategories, and a subcategory can have one parent category;
            builder.Entity<CategoryModel>()
                .HasMany(parent => parent.SubCategories)
                .WithOne(child => child.ParentCategory)
                .HasForeignKey(child => child.ParentCategoryId);

            // Create a one to many relationship between a product and its ProductReviews.
            // A product can have many reviews, but a review can only 'review' one product.
            builder.Entity<ProductReviewModel>()
                .HasOne(review => review.Product)
                .WithMany(product => product.ProductReviews)
                .HasForeignKey(review => review.ProductId);

            // Create a one to many relationship between a product and its ProductRatings.
            // A product can have many ratings, but a rating can only 'rate' one product.
            builder.Entity<ProductRatingModel>()
                .HasOne(rating => rating.Product)
                .WithMany(product => product.ProductRatings)
                .HasForeignKey(rating => rating.ProductId);

        }

        // DbSets to add to database.
        public DbSet<ElectroShop.Models.OrderDetailModel> OrderDetailModel { get; set; }
    }
}
