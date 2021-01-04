using Integracja.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(g => g.RowVersion)
               .IsRowVersion();

            builder.Property(g => g.Name)
                .IsRequired();

            builder.HasOne(g => g.Author)
                .WithMany(u => u.CreatedGames)
                .HasForeignKey(g => g.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
