using AutoMapper;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Web.Models.Shared.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class HistoryUserModelProfile : Profile
    {
        public HistoryUserModelProfile()
        {
            //CreateMap<GameUser, HistoryUserModel>()
            //  .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
            //  .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            //  .ForMember(dest => dest.GameScore, opt => opt.MapFrom(src => src.GameScore))
            //  .ForMember(dest => dest.CorrectlyAnsweredQuestions, opt => opt.MapFrom(src => src.CorrectlyAnsweredQuestions))
            //  .ForMember(dest => dest.IncorrectlyAnsweredQuestions, opt => opt.MapFrom(src => src.IncorrectlyAnsweredQuestions));


        }
    }
}
