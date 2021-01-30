using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Joins;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Gamemode> Gamemodes { get; set; }
        public DbSet<GameQuestion> GameQuestions { get; set; }
        public DbSet<GameUser> GameUsers { get; set; }
        public DbSet<GameUserQuestion> GameUserQuestions { get; set; }
        public DbSet<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
