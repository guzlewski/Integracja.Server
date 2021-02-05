using Integracja.Server.Core.Models.Joins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Infrastructure.Data.Configuration
{
    public class GameQuestionConfiguration : IEntityTypeConfiguration<GameQuestion>
    {
        public void Configure(EntityTypeBuilder<GameQuestion> builder)
        {
            builder.HasKey(gq => new { gq.GameId, gq.QuestionId });
        }
    }
}