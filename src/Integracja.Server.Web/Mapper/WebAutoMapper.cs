using AutoMapper;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Mapper.Profiles;
using Integracja.Server.Web.Models.Shared.Answer;
using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Game;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Mapper
{
    public static class WebAutoMapper
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CategoryModelProfile>();
                cfg.AddProfile<AnswerModelProfile>();
                cfg.AddProfile<QuestionModelProfile>();
                cfg.AddProfile<GamemodeModelProfile>();
                cfg.AddProfile<GameModelProfile>();
                cfg.AddProfile<GameSettingsModelProfile>();
            })
            .CreateMapper();
        }
    }
}
