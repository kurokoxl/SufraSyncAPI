using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SufraSyncAPI.Models.Entities;

namespace SufraSync.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {

            builder.HasKey(ci => new { ci.UserId, ci.ProductId });

            // Relationship: User
            builder.HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId);

            // Relationship: Product
            builder.HasOne(ci => ci.Product)
                .WithMany() 
                .HasForeignKey(ci => ci.ProductId);
        }
    }
}