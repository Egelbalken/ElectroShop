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
    }
}
