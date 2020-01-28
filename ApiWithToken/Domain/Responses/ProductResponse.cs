using ApiWithToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Responses
{
    public class ProductResponse : BaseResponse
    {
        public Product product { get; set; }

        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            this.product = product;
        }

        //BAŞARILI OLURSA DÖNECEK OLAN RESPONSE
        public ProductResponse(Product product) : this(true, string.Empty, product)
        {
        }

        //BAŞARISIZ OLURSA DÖNECEK OLAN RESPONSE
        public ProductResponse(string message) : this(false, message, null)
        {
        }
    }
}