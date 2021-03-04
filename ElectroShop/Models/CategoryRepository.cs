using System.Collections.Generic;
using System.Linq;
using ElectroShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IEnumerable<CategoryModel> AllCategories => applicationDbContext.Categories.ToList();

        public CategoryModel GetCategory(int id)
        {
            return applicationDbContext.Categories
                .Include(category => category.SubCategories)
                .Include(category => category.ParentCategory)
                .Include(category => category.Products)
                .FirstOrDefault(category => category.CategoryId == id);
        }

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
