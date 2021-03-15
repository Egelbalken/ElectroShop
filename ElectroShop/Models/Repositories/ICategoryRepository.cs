using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// Repository for the CategoryModel
    /// </summary>
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> AllCategories { get; }
        IEnumerable<CategoryModel> GetAllTopLevelCategories();
        IEnumerable<ProductModel> GetAllProducts(int categoryId);
        CategoryModel GetCategory(int id);
    }
}
