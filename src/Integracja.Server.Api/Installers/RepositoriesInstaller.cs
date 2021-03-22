using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Api.Installers
{
    public class RepositoriesInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IGamemodeRepository, GamemodeRepository>();

            services.AddScoped<IQuestionRepository, QuestionRepository>();

            services.AddScoped<IGameRepository, GameRepository>();

            services.AddScoped<IGameUserRepository, GameUserRepository>();

            services.AddScoped<IGameLogicRepository, GameLogicRepository>();
        }
    }
}
