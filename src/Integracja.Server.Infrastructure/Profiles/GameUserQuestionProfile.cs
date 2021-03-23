using AutoMapper;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class GameUserQuestionProfile : Profile
    {
        public GameUserQuestionProfile()
        {
            CreateMap<GameUserQuestion, GameUserQuestionDto<AnswerDto>>()
                .ForMember(
                    gameUserQuestionDto => gameUserQuestionDto.GameOver,
                    opt => opt.MapFrom(gameUser => gameUser.GameUser.GameOver)); 
            
            CreateMap<GameUserQuestion, GameUserQuestionDto<DetailAnswerDto>>()
                .ForMember(
                    gameUserQuestionDto => gameUserQuestionDto.GameOver,
                    opt => opt.MapFrom(gameUser => gameUser.GameUser.GameOver));
        }
    }
}
