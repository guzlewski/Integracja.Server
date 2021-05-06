using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class QuestionModelProfile : Profile
    {
        public QuestionModelProfile()
        {
            CreateMap<QuestionModel, CreateQuestionDto>()
            .ForMember(dest => dest.QuestionScoring, opt => opt.MapFrom(src => src.Scoring));

            CreateMap<QuestionModel, EditQuestionDto>()
            .ForMember(dest => dest.QuestionScoring, opt => opt.MapFrom(src => src.Scoring));

            CreateMap<DetailQuestionDto<AnswerDto>, QuestionModel>()
            .ForMember(dest => dest.Scoring, opt => opt.MapFrom(src => src.QuestionScoring));

            CreateMap<QuestionDto, QuestionModel>()
            .ForMember(dest => dest.Scoring, opt => opt.MapFrom(src => src.QuestionScoring));


            CreateMap<Question, QuestionModel>()
            .ForMember(dest => dest.Scoring, opt => opt.MapFrom(src => src.QuestionScoring))
            .ForMember(dest => dest.NegativePoints, opt => opt.MapFrom(src => (int)src.NegativePoints))
            .ForMember(dest => dest.PositivePoints, opt => opt.MapFrom(src => (int)src.PositivePoints))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<GameQuestion, QuestionModel>().IncludeMembers(src => src.Question);


            CreateMap<QuestionModel, CreateGameQuestionDto>()
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
