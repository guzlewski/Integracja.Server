using AutoMapper;
using Integracja.Server.Core.Models.Base;
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
            CreateMap<Game, HistoryUserModel>()
              .ForMember(dest => dest.GameUsers, opt => opt.MapFrom(src => src.GameUsers));

            CreateMap<GameUser, HistoryUserModel>()
              .ForMember(dest => dest.UserAnswerPool, opt => opt.MapFrom(src => src.GameUserQuestionAnswers));
        }
    }
}
