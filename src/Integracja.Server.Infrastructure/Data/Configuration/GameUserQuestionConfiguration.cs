using Integracja.Server.Core.Models.Joins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Infrastructure.Data.Configuration
{
    public class GameUserQuestionConfiguration : IEntityTypeConfiguration<GameUserQuestion>
    {
        public void Configure(EntityTypeBuilder<GameUserQuestion> builder)
        {
            builder.HasKey(guq => new { guq.GameId, guq.UserId, guq.QuestionId });

            builder.HasOne(guq => guq.GameUser)
                .WithMany(gu => gu.GameUserQuestions)
                .HasForeignKey(guq => new { guq.GameId, guq.UserId })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(guq => guq.GameQuestion)
                .WithMany(gq => gq.GameUserQuestions)
                .HasForeignKey(guq => new { guq.GameId, guq.QuestionId })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}