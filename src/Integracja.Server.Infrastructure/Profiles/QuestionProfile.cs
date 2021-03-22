using System.Linq;
using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>()
                .ForMember(
                    questionDto => questionDto.CorrectAnswersCount,
                    opt => opt.MapFrom(question => question.Answers.Where(a => a.IsCorrect).Count()));

            CreateMap<Question, DetailQuestionDto<AnswerDto>>();

            CreateMap<Question, DetailQuestionDto<DetailAnswerDto>>();

            CreateMap<CreateQuestionDto, Question>();

            CreateMap<EditQuestionDto, Question>();
        }
    }
}
