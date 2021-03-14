using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerDto>();

            CreateMap<Answer, DetailAnswerDto>();

            CreateMap<CreateAnswerDto, Answer>();

            CreateMap<EditAnswerDto, Answer>();
        }
    }
}
