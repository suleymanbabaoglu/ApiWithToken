using ApiWithToken.Domain.Extensions;
using ApiWithToken.Domain.Models;
using ApiWithToken.Domain.Resources;
using ApiWithToken.Domain.Responses;
using ApiWithToken.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiWithToken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> productService;
        private readonly IMapper mapper;

        public ProductController(IService<Product> productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            BaseResponse<IEnumerable<Product>> productListResponse = await productService.GetWhere(x => x.Id > 0);

            if (productListResponse.Success)
            {
                return Ok(productListResponse.Extra);
            }
            else
            {
                return BadRequest(productListResponse.ErrorMessage);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            BaseResponse<Product> productResponse = await productService.GetById(id);
            if (productResponse.Success)
            {
                return Ok(productResponse.Extra);
            }
            else
            {
                return BadRequest(productResponse.Extra);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody]ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                var price = Convert.ToDecimal(productResource.Price);
                Product product = mapper.Map<ProductResource, Product>(productResource);
                product.Price = price;
                BaseResponse<Product> productResponse = await productService.Add(product);

                if (productResponse.Success)
                {
                    return Ok(productResponse.Extra);
                }
                else
                {
                    return BadRequest(productResponse.ErrorMessage);
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductResource productResource, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = mapper.Map<ProductResource, Product>(productResource);
                product.Id = id;

                BaseResponse<Product> productResponse = await productService.Update(product);

                if (productResponse.Success)
                {
                    return Ok(productResponse.Extra);
                }
                else
                {
                    return BadRequest(productResponse.ErrorMessage);
                }
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            BaseResponse<Product> productResponse = await productService.Delete(id);

            if (productResponse.Success)
            {
                return Ok(productResponse.Extra);
            }
            else
            {
                return BadRequest(productResponse.ErrorMessage);
            }
        }
    }
}