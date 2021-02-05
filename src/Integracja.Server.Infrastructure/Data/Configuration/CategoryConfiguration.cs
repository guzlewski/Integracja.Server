using Integracja.Server.Core.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.RowVersion)
                .IsConcurrencyToken();

            builder.Property(c => c.Name)
                .IsRequired();

            builder.HasOne(c => c.Owner)
                .WithMany(u => u.CreatedCategories)
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}