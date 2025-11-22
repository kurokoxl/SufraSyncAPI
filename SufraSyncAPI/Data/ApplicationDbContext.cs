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

            // Apply all entity configurations from the current assembly
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            LoadData(builder);
        }

        public void LoadData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, Name = "Appetizer" },
              new Category { CategoryId = 2, Name = "Entree" },
              new Category { CategoryId = 3, Name = "Side Dish" },
              new Category { CategoryId = 4, Name = "Dessert" },
              new Category { CategoryId = 5, Name = "Beverage" }
          );

            modelBuilder.Entity<Ingredient>().HasData(
              //add mexican restaurant ingredients here
              new Ingredient { IngredientId = 1, Name = "Beef", Stock = 500 },
              new Ingredient { IngredientId = 2, Name = "Chicken", Stock = 600 },
              new Ingredient { IngredientId = 3, Name = "Fish", Stock = 300 },
              new Ingredient { IngredientId = 4, Name = "Tortilla", Stock = 1000 },
              new Ingredient { IngredientId = 5, Name = "Lettuce", Stock = 200 },
              new Ingredient { IngredientId = 6, Name = "Tomato", Stock = 250 }
          );

            modelBuilder.Entity<Product>().HasData(

                //Add mexican restaurant food entries here
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
                new ProductIngredient { ProductId = 1, IngredientId = 1, QuantityRequired = 0.1m, Unit = "kg" },
                new ProductIngredient { ProductId = 1, IngredientId = 4, QuantityRequired = 2m, Unit = "pcs" },
                new ProductIngredient { ProductId = 1, IngredientId = 5, QuantityRequired = 0.05m, Unit = "kg" },
                new ProductIngredient { ProductId = 1, IngredientId = 6, QuantityRequired = 0.03m, Unit = "kg" },
                new ProductIngredient { ProductId = 2, IngredientId = 2, QuantityRequired = 0.09m, Unit = "kg" },
                new ProductIngredient { ProductId = 2, IngredientId = 4, QuantityRequired = 2m, Unit = "pcs" },
                new ProductIngredient { ProductId = 2, IngredientId = 5, QuantityRequired = 0.05m, Unit = "kg" },
                new ProductIngredient { ProductId = 2, IngredientId = 6, QuantityRequired = 0.03m, Unit = "kg" },
                new ProductIngredient { ProductId = 3, IngredientId = 3, QuantityRequired = 0.12m, Unit = "kg" },
                new ProductIngredient { ProductId = 3, IngredientId = 4, QuantityRequired = 2m, Unit = "pcs" },
                new ProductIngredient { ProductId = 3, IngredientId = 5, QuantityRequired = 0.05m, Unit = "kg" },
                new ProductIngredient { ProductId = 3, IngredientId = 6, QuantityRequired = 0.03m, Unit = "kg" }
                );
        }
    }
}
