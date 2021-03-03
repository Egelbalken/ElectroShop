using ElectroShop.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> AllProducts { get; }

        IEnumerable<ProductModel> OnSaleProduct { get; }

        ProductModel GetProduct(int productId);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IEnumerable<CategoryModel> AllCategories => applicationDbContext.Categories.ToList();

        public IEnumerable<ProductModel> AllProducts => throw new NotImplementedException();

        public IEnumerable<ProductModel> OnSaleProduct => throw new NotImplementedException();

        public ProductModel GetProduct(int id)
        {
            return applicationDbContext.Products
                .Include(product => product.Category)
                .FirstOrDefault(product => product.ProductId == id);
        }

        //public ProductModel GetProduct(int productId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}


