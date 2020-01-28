using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Resources
{
    public class ProductResource
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [MaxLength(8)]
        public string? Category { get; set; }

        [Required]
        [MinLength(3)]
        public decimal? Price { get; set; }
    }
}