using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> AllCategories { get; }

        CategoryModel GetCategory(int id);

    }
}
