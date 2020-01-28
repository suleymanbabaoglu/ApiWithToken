using ApiWithToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();

        Task AddProductAsync(Product product);

        Task RemoveProductAsync(int productId);

        void UpdateProductAsync(Product product);

        Task<Product> FindByIdAsync(int productId);
    }
}