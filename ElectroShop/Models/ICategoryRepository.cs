using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> AllCategories { get; }

        CategoryModel GetCategory(int id);

    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IEnumerable<CategoryModel> AllCategories => throw new NotImplementedException();

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
