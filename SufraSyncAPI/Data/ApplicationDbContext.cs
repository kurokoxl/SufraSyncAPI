using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SufraSyncAPI.Models.Entities;
using System.Reflection;

namespace SufraSync.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            LoadData(builder);
        }

        public void LoadData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole
        {
            Id = "c7b013f0-5201-4317-abd8-c211f91b7330",
            Name = "Admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = "1"
        },
        new IdentityRole
        {
            Id = "bd5865a2-27c2-4a38-a203-392833302a59",
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = "2"
        }
    );
            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, Name = "Appetizer" },
              new Category { CategoryId = 2, Name = "Entree" },
              new Category { CategoryId = 3, Name = "Side Dish" },
              new Category { CategoryId = 4, Name = "Dessert" },
              new Category { CategoryId = 5, Name = "Beverage" }
          );

                        modelBuilder.Entity<Ingredient>().HasData(
                            new Ingredient { IngredientId = 1, Name = "Beef", Stock = 500, Unit = "kg" },
                            new Ingredient { IngredientId = 2, Name = "Chicken", Stock = 600, Unit = "kg" },
                            new Ingredient { IngredientId = 3, Name = "Fish", Stock = 300, Unit = "kg" },
                            new Ingredient { IngredientId = 4, Name = "Tortilla", Stock = 1000, Unit = "pcs" },
                            new Ingredient { IngredientId = 5, Name = "Lettuce", Stock = 200, Unit = "kg" },
                            new Ingredient { IngredientId = 6, Name = "Tomato", Stock = 250, Unit = "kg" }
                    );

            modelBuilder.Entity<Product>().HasData(

                new Product
                {
                    ProductId = 1,
                    Name = "Beef Taco",
                    Description = "A delicious beef taco",
                    Price = 2.50m,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Chicken Taco",
                    Description = "A delicious chicken taco",
                    Price = 1.99m,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Fish Taco",
                    Description = "A delicious fish taco",
                    Price = 3.99m,
                    CategoryId = 2
                }
                );

            modelBuilder.Entity<ProductIngredient>().HasData(
                new ProductIngredient { ProductId = 1, IngredientId = 1, QuantityRequired = 0.1m },
                new ProductIngredient { ProductId = 1, IngredientId = 4, QuantityRequired = 2m },
                new ProductIngredient { ProductId = 1, IngredientId = 5, QuantityRequired = 0.05m },
                new ProductIngredient { ProductId = 1, IngredientId = 6, QuantityRequired = 0.03m },
                new ProductIngredient { ProductId = 2, IngredientId = 2, QuantityRequired = 0.09m },
                new ProductIngredient { ProductId = 2, IngredientId = 4, QuantityRequired = 2m },
                new ProductIngredient { ProductId = 2, IngredientId = 5, QuantityRequired = 0.05m },
                new ProductIngredient { ProductId = 2, IngredientId = 6, QuantityRequired = 0.03m },
                new ProductIngredient { ProductId = 3, IngredientId = 3, QuantityRequired = 0.12m },
                new ProductIngredient { ProductId = 3, IngredientId = 4, QuantityRequired = 2m },
                new ProductIngredient { ProductId = 3, IngredientId = 5, QuantityRequired = 0.05m },
                new ProductIngredient { ProductId = 3, IngredientId = 6, QuantityRequired = 0.03m }
                );
        }
    }
}
