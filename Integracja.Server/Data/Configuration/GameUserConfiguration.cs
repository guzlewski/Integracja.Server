using Integracja.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Data.Configuration
{
    public class GameUserConfiguration : IEntityTypeConfiguration<GameUser>
    {
        public void Configure(EntityTypeBuilder<GameUser> builder)
        {
            builder.HasKey(gu => new { gu.GameId, gu.UserId });
        }
    }
}