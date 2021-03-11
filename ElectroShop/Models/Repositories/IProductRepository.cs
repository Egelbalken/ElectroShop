using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> AllProducts { get; }

        IEnumerable<ProductModel> OnSaleProduct { get; }

        ProductModel GetProduct(int productId);
    }
}


