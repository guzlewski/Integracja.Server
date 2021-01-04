using Integracja.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Data.Configuration
{
    public class GameQuestionConfiguration : IEntityTypeConfiguration<GameQuestion>
    {
        public void Configure(EntityTypeBuilder<GameQuestion> builder)
        {
            builder.HasKey(gq => new { gq.GameId, gq.QuestionId });
        }
    }
}