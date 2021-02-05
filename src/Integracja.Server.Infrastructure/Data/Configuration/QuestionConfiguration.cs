using Integracja.Server.Core.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Infrastructure.Data.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.RowVersion)
                .IsConcurrencyToken();

            builder.Property(q => q.Content)
                .IsRequired();

            builder.HasOne(q => q.Owner)
                .WithMany(u => u.CreatedQuestions)
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}