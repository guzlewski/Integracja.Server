using Integracja.Server.Core.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Infrastructure.Data.Configuration
{
    public class GamemodeConfiguration : IEntityTypeConfiguration<Gamemode>
    {
        public void Configure(EntityTypeBuilder<Gamemode> builder)
        {
            builder.Property(gm => gm.RowVersion)
                .IsConcurrencyToken();

            builder.Property(gm => gm.Name)
                .IsRequired();

            builder.HasOne(gm => gm.Author)
                .WithMany(u => u.CreatedGameModes)
                .HasForeignKey(gm => gm.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(gm => !gm.IsDeleted);
        }
    }
}