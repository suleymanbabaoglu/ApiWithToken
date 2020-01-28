using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Models
{
    public class ApiWithTokenDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public IConfiguration Configuration { get; private set; }

        public ApiWithTokenDBContext(IConfiguration configuration)
        : base()
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnectionString"];

            optionsBuilder
                .UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Name = "admin",
                    Password = "admin",
                    Email = "sss@sss.com"
                });

            modelBuilder.Entity<Product>()
           .HasData(new Product
           {
               Id = 1,
               Name = "admin",
               Category = "admin",
               Price = 0
           });
        }
    }
}