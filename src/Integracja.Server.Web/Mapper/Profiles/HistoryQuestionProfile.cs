using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Web.Models.Shared.History;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class HistoryQuestionProfile : Profile
    {
        public HistoryQuestionProfile()
        {
            CreateMap<Game, HistoryQuestionModel>()
               .ForMember(dest => dest.QuestionPool, opt => opt.MapFrom(src => src.Questions));
        }
    }
}
