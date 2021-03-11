using System.Collections.Generic;
using System.Linq;
using ElectroShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        /// <summary>
        /// Constructur injection of the database context. 
        /// </summary>
        /// <param name="applicationDbContext">The DbContext to be used.</param>
        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Get all categories from the context.
        /// </summary>
        public IEnumerable<CategoryModel> AllCategories => applicationDbContext.Categories.ToList();

        /// <summary>
        /// Fetch the category with the selected category ID. 
        /// Includes subcategories, parent categoryh and products.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>The selected CategoryModel, null if not found.</returns>
        public CategoryModel GetCategory(int id)
        {
            return applicationDbContext.Categories
                .Include(category => category.SubCategories)
                .Include(category => category.ParentCategory)
                .Include(category => category.Products)
                .FirstOrDefault(category => category.CategoryId == id);
        }

        /// <summary>
        /// Recursively retrieves all products from the selected category, and all its descendant subcategories.
        /// In a depth-first search manner.
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <returns>IEnumerable of ProductModel</returns>
        public IEnumerable<ProductModel> GetAllProducts(int categoryId)
        {
            var products = new List<ProductModel>();
            var category = applicationDbContext.Categories
                .Include(category => category.Products)
                .Include(category => category.SubCategories)
                .SingleOrDefault(category => category.CategoryId == categoryId);

            if (category != default)
            {
                products.AddRange(category.Products);
                foreach (var subCategory in category.SubCategories)
                {
                    products.AddRange(GetAllProducts(subCategory.CategoryId));
                }
            }

            return products;
        }
    }
}
