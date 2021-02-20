using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnswerDto, Answer>();
                cfg.CreateMap<QuestionAdd, Question>();
                cfg.CreateMap<QuestionModify, Question>();
                cfg.CreateMap<CategoryAdd, Category>();
                cfg.CreateMap<CategoryModify, Category>();
                cfg.CreateMap<GamemodeAdd, Gamemode>();
                cfg.CreateMap<GamemodeModify, Gamemode>();
                cfg.CreateMap<GameAdd, Game>();
                cfg.CreateMap<GameModify, Game>();
            })
            .CreateMapper();
        }
    }
}
