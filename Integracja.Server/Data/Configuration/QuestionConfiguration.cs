using Integracja.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Data.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.RowVersion)
               .IsRowVersion();

            builder.Property(q => q.Content)
                .IsRequired();

            builder.HasOne(q => q.Author)
                .WithMany(u => u.CreatedQuestions)
                .HasForeignKey(g => g.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}