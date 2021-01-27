using Integracja.Server.Core.Models.Joins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Infrastructure.Configuration
{
    public class GameUserConfiguration : IEntityTypeConfiguration<GameUser>
    {
        public void Configure(EntityTypeBuilder<GameUser> builder)
        {
            builder.HasKey(gu => new { gu.GameId, gu.UserId });
        }
    }
}