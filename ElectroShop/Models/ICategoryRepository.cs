using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> AllCategories { get; }
        IEnumerable<ProductModel> GetAllProducts(int categoryId);
        CategoryModel GetCategory(int id);
    }
}
