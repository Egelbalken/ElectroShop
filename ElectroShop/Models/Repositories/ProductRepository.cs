﻿using ElectroShop.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectroShop.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Get all ProductModels from the database context.
        /// </summary>
        public IEnumerable<ProductModel> AllProducts => applicationDbContext.Products.ToList();

        public IEnumerable<ProductModel> OnSaleProduct => throw new NotImplementedException();

        /// <summary>
        /// Get the requested ProductModel.
        /// </summary>
        /// <param name="id">The ProductModel ID</param>
        /// <returns>The requested ProductModel</returns>
        public ProductModel GetProduct(int id)
        {
            return applicationDbContext.Products
                .Include(product => product.Category)
                .FirstOrDefault(product => product.ProductId == id);
        }
    }
}


