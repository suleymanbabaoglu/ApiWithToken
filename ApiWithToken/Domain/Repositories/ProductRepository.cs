using ApiWithToken.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(ApiWithTokenDBContext context) : base(context)
        {
        }

        public async Task AddProductAsync(Product product)
        {
            await context.Products.AddAsync(product);
        }

        public async Task<Product> FindByIdAsync(int productId)
        {
            return await context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            context.Products.Remove(product);
        }

        public void UpdateProductAsync(Product product)
        {
            context.Products.Update(product);
        }
    }
}