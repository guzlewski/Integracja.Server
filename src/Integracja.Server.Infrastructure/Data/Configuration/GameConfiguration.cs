using Integracja.Server.Core.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Infrastructure.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(g => g.RowVersion)
                .IsConcurrencyToken();

            builder.Property(g => g.Name)
                .IsRequired();

            builder.HasIndex(g => g.Guid)
                .IsUnique();

            builder.HasOne(g => g.Owner)
                .WithMany(u => u.CreatedGames)
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}