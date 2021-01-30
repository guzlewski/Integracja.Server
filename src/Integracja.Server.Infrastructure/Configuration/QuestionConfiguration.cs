using Integracja.Server.Core.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Infrastructure.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.Timestamp)
                .IsRowVersion();

            builder.Property(q => q.Content)
                .IsRequired();

            builder.HasOne(q => q.Author)
                .WithMany(u => u.CreatedQuestions)
                .HasForeignKey(g => g.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(q => !q.IsDeleted);
        }
    }
}