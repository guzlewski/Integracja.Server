using Integracja.Server.Infrastructure.Services.Implementations;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Api.Installers
{
    public class ServicesInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IGamemodeService, GamemodeService>();

            services.AddScoped<IQuestionService, QuestionService>();

            services.AddScoped<IGameService, GameService>();

            services.AddScoped<IGameUserService, GameUserService>();

            services.AddScoped<IGameLogicService, GameLogicService>();

            services.AddSingleton<IStorageService, BlobStorageService>();

            services.AddScoped<IPictureService, PictureService>();
        }
    }
}
