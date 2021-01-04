using Integracja.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.RowVersion)
                .IsRowVersion();

            builder.Property(c => c.Name)
                .IsRequired();

            builder.HasOne(c => c.Author)
                .WithMany(u => u.CreatedCategories)
                .HasForeignKey(g => g.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
