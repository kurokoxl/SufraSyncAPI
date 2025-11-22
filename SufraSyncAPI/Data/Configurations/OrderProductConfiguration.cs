using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SufraSyncAPI.Models.Entities;

namespace SufraSync.Data.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(oi => new { oi.ProductId, oi.OrderId });

            // Relationship with Product
            builder.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            // Relationship with Order
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}