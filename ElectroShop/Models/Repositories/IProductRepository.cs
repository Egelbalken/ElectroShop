using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// Repository for the ProductModel
    /// </summary>
    public interface IProductRepository
    {
        IEnumerable<ProductModel> AllProducts { get; }

        IEnumerable<ProductModel> OnSaleProduct { get; }

        ProductModel GetProduct(int productId);
    }
}


