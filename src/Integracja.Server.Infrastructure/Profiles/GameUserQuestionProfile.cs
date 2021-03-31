using System.Linq;
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
                    opt => opt.MapFrom(gameUserQuestion => gameUserQuestion.GameUser.GameOver));

            CreateMap<GameUserQuestion, GameUserQuestionDto<DetailAnswerDto>>()
                .ForMember(
                    gameUserQuestionDto => gameUserQuestionDto.GameOver,
                    opt => opt.MapFrom(gameUserQuestion => gameUserQuestion.GameUser.GameOver));

            CreateMap<GameUserQuestion, DetailGameUserQuestionDto>()
                .ForMember(
                    gameUserQuestionDto => gameUserQuestionDto.SelectedAnswers,
                    opt => opt.MapFrom(gameUserQuestion => gameUserQuestion.GameUserQuestionAnswers.Select(guqa => guqa.AnswerId)));
        }
    }
}
