using AutoMapper;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Web.Models.Shared.Answer;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class UserAnswerModelProfile : Profile
    {
        public UserAnswerModelProfile()
        {
            CreateMap<GameUserQuestionAnswer, UserAnswerModel>()
                .ForMember(dest => dest.UserQuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.UserAnswerId, opt => opt.MapFrom(src => src.AnswerId));

        }
    }
}
