using ApiWithToken.Domain.Models;
using ApiWithToken.Domain.Resources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductResource>();
            CreateMap<ProductResource, Product>();
        }
    }
}