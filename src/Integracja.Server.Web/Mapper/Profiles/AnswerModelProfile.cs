using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Answer;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class AnswerModelProfile : Profile
    {
        public AnswerModelProfile()
        {

            CreateMap<AnswerModel, AnswerDto>();
            CreateMap<AnswerDto, AnswerModel>();

            CreateMap<AnswerModel, DetailAnswerDto>();
            CreateMap<DetailAnswerDto, AnswerModel>();

            CreateMap<AnswerModel, EditAnswerDto>();
            CreateMap<EditAnswerDto, AnswerModel>();

            CreateMap<AnswerModel, CreateAnswerDto>();
            CreateMap<CreateAnswerDto, AnswerModel>();


            CreateMap<Answer, AnswerModel>();
            CreateMap<AnswerModel, Answer>();
        }
    }
}
