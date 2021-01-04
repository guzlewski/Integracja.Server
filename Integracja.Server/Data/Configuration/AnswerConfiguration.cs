using Integracja.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracja.Server.Data.Configuration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(a => a.RowVersion)
                .IsRowVersion();

            builder.Property(a => a.Content)
                .IsRequired();
        }
    }
}
