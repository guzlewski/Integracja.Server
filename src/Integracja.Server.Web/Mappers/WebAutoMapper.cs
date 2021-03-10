using AutoMapper;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Answer;
using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Mappers
{
    public static class WebAutoMapper
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnswerModel, AnswerDto>();
                cfg.CreateMap<AnswerDto, AnswerModel>();
                cfg.CreateMap<AnswerModel, DetailAnswerDto>();
                cfg.CreateMap<DetailAnswerDto, AnswerModel>();
                cfg.CreateMap<AnswerModel, EditAnswerDto>();
                cfg.CreateMap<EditAnswerDto, AnswerModel>();
                cfg.CreateMap<AnswerModel, CreateAnswerDto>();
                cfg.CreateMap<CreateAnswerDto, AnswerModel>();


                cfg.CreateMap<QuestionModel, CreateQuestionDto>()
                .ForMember( dest => dest.QuestionScoring, opt => opt.MapFrom( src => src.Scoring ) );
                cfg.CreateMap<QuestionModel, EditQuestionDto>()
                .ForMember(dest => dest.QuestionScoring, opt => opt.MapFrom(src => src.Scoring));
                cfg.CreateMap<DetailQuestionDto, QuestionModel>();
                cfg.CreateMap<QuestionDto, QuestionModel>();

                cfg.CreateMap<CategoryModel, CreateCategoryDto>();
                cfg.CreateMap<CategoryModel, EditCategoryDto>();
                cfg.CreateMap<CategoryDto, CategoryModel>();
                cfg.CreateMap<DetailCategoryDto, CategoryModel>();

                cfg.CreateMap<DetailGamemodeDto, GamemodeModel>();
                cfg.CreateMap<GamemodeDto, GamemodeModel>();
                cfg.CreateMap<GamemodeModel, CreateGamemodeDto>();
                cfg.CreateMap<GamemodeModel, EditGamemodeDto>();
                

            })
            .CreateMapper();
        }
    }
}
