using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}