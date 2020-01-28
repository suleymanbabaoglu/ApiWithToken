using ApiWithToken.Domain.Models;
using ApiWithToken.Domain.Repositories;
using ApiWithToken.Domain.Responses;
using ApiWithToken.Domain.Services;
using ApiWithToken.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> AddProduct(Product product)
        {
            try
            {
                await productRepository.AddProductAsync(product);
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün Eklenirken Bir Hata Oluştu::{ex.Message}");
            }
        }

        public async Task<ProductResponse> FindByIdAsync(int productId)
        {
            try
            {
                Product product = await productRepository.FindByIdAsync(productId);
                if (product == null)
                {
                    return new ProductResponse($"Ürün Bulunamadı");
                }
                else
                {
                    return new ProductResponse(product);
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün Bulunurken Bir Hata Oluştu::{ex.Message}");
            }
        }

        public async Task<ProductListResponse> ListAsync()
        {
            try
            {
                IEnumerable<Product> productList = await productRepository.ListAsync();
                return new ProductListResponse(productList);
            }
            catch (Exception ex)
            {
                return new ProductListResponse($"Ürün Listesi Alınırken Bir Hata Oluştu::{ex.Message}");
            }
        }

        public async Task<ProductResponse> RemoveProduct(int productId)
        {
            try
            {
                Product product = await productRepository.FindByIdAsync(productId);
                if (product == null)
                {
                    return new ProductResponse($"Silmeye Çalıştığınız Ürün Bulunamamıştır...");
                }
                else
                {
                    await productRepository.RemoveProductAsync(productId);
                    await unitOfWork.CompleteAsync();
                    return new ProductResponse(product);
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün Silinirken Bir Hata Oluştu...::{ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateProduct(Product product, int productId)
        {
            try
            {
                var updatingProduct = await productRepository.FindByIdAsync(productId);
                if (updatingProduct == null)
                {
                    return new ProductResponse("Güncellemeye Çalıştığınız Ürün Bulunamadı...");
                }
                updatingProduct.Name = product.Name;
                updatingProduct.Category = product.Category;
                updatingProduct.Price = product.Price;

                productRepository.UpdateProductAsync(updatingProduct);

                await unitOfWork.CompleteAsync();

                return new ProductResponse(updatingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün Güncellenirken Bir Hata Oluştu::{ex.Message}");
            }
        }
    }
}