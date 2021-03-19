using AutoMapper;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Answer;
using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Game;
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
                cfg.CreateMap<DetailQuestionDto, QuestionModel>()
                .ForMember(dest => dest.Scoring, opt => opt.MapFrom(src => src.QuestionScoring));
                cfg.CreateMap<QuestionDto, QuestionModel>()
                .ForMember(dest => dest.Scoring, opt => opt.MapFrom(src => src.QuestionScoring));

                cfg.CreateMap<CategoryModel, CreateCategoryDto>();
                cfg.CreateMap<CategoryModel, EditCategoryDto>();
                cfg.CreateMap<CategoryDto, CategoryModel>();
                cfg.CreateMap<DetailCategoryDto, CategoryModel>();

                cfg.CreateMap<DetailGamemodeDto, GamemodeModel>();
                cfg.CreateMap<GamemodeDto, GamemodeModel>();
                cfg.CreateMap<GamemodeModel, CreateGamemodeDto>();
                cfg.CreateMap<GamemodeModel, EditGamemodeDto>();

                cfg.CreateMap<QuestionModel, CreateGameQuestionDto>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id));

                cfg.CreateMap<GameModel, CreateGameDto>()
                .ForMember(dest => dest.MaxPlayers, opt => opt.MapFrom(src => src.Settings.MaxPlayersCount))
                .ForMember(dest => dest.RandomizeQuestionOrder, opt => opt.MapFrom(src => src.Settings.RandomizeQuestionOrder))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Settings.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Settings.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Settings.EndTime))
                .ForMember(dest => dest.GamemodeId, opt => opt.MapFrom(src => src.Settings.GamemodeId))
                .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.QuestionPool.Count));

                cfg.CreateMap<GameDto, GameSettingsModel>()
                .ForMember(dest => dest.MaxPlayersCount, opt => opt.MapFrom(src => src.MaxPlayersCount))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.GamemodeId, opt => opt.MapFrom(src => src.Gamemode.Id));
            })
            .CreateMapper();
        }
    }
}
