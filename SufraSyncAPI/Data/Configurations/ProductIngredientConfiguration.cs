using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SufraSyncAPI.Models.Entities;

namespace SufraSync.Data.Configurations
{
    public class ProductIngredientConfiguration : IEntityTypeConfiguration<ProductIngredient>
    {
        public void Configure(EntityTypeBuilder<ProductIngredient> builder)
        {
            // Composite primary key
            builder.HasKey(pi => new { pi.ProductId, pi.IngredientId });

            // Many-to-many relationship with Product
            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            // Many-to-many relationship with Ingredient
            builder.HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            // Configure QuantityRequired precision to avoid truncation
            builder.Property(pi => pi.QuantityRequired)
                .HasPrecision(18, 3);

            // Configure Unit column
            builder.Property(pi => pi.Unit)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}