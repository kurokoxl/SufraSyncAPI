using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SufraSyncAPI.Models.Entities;

namespace SufraSync.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // One-to-many relationship with Orders
            builder.HasMany(u => u.orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);
        }
    }
}