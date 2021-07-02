using api_rest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
using api_rest.Domain.Helpers;

namespace api_rest.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<User> users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().ToTable("categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category)
                .HasForeignKey(p => p.IdCategory);

            builder.Entity<Category>().HasData(
                new Category { Id = 100, Name = "Fruits and Vegetables" },
                new Category { Id = 101, Name = "Dairy" }
            );

            builder.Entity<Product>().ToTable("products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Product>().Property(p => p.UnitOfMeasurement);
            builder.Entity<Product>().Property(p => p.QuantityInPackge).IsRequired();
            builder.Entity<Product>().Property(p => p.IdCategory);
            

            builder.Entity<Product>().HasData(
                new Product 
                { 
                    Id = 100, 
                    Name = "Banana", 
                    IdCategory = 100, 
                    QuantityInPackge = 10,
                    UnitOfMeasurement = EUnitOfMeasurement.Kilogram
                }
            );

            builder.Entity<User>().ToTable("users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Entity<User>().Property(p => p.Name).HasMaxLength(30);
            builder.Entity<User>().Property(p => p.Username).HasMaxLength(15).IsRequired();
            builder.Entity<User>().Property(p => p.Password).HasMaxLength(12).IsRequired();
            builder.Entity<User>().Property(p => p.TypeUser).IsRequired();

            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Administrador",
                    Username = "Admim",
                    Password = "Admim",
                    TypeUser = 1
                });

        }
    }

}
