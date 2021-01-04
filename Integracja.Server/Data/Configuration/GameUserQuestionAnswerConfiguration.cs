using Integracja.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Data.Configuration
{
    public class GameUserQuestionAnswerConfiguration : IEntityTypeConfiguration<GameUserQuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<GameUserQuestionAnswer> builder)
        {
            builder.HasKey(guqa => new { guqa.GameId, guqa.UserId, guqa.QuestionId, guqa.AnswerId });

            builder.HasOne(guqa => guqa.GameUserQuestion)
               .WithMany(guq => guq.GameUserQuestionAnswers)
               .HasForeignKey(guqa => new { guqa.GameId, guqa.UserId, guqa.QuestionId })
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(guqa => guqa.GameQuestion)
               .WithMany(gq => gq.GameUserQuestionAnswers)
               .HasForeignKey(guqa => new { guqa.GameId, guqa.QuestionId })
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(guqa => guqa.GameUser)
                .WithMany(gu => gu.GameUserQuestionAnswers)
                .HasForeignKey(guqa => new { guqa.GameId, guqa.UserId })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(guqa => guqa.Question)
                .WithMany(q => q.GameUserQuestionAnswers)
                .HasForeignKey(guqa => guqa.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
