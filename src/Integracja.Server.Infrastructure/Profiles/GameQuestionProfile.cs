using AutoMapper;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class GameQuestionProfile : Profile
    {
        public GameQuestionProfile()
        {
            CreateMap<CreateGameQuestionDto, GameQuestion>();
        }
    }
}
