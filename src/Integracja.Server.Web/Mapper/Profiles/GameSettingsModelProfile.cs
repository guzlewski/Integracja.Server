using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Game;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class GameSettingsModelProfile : Profile
    {
        public GameSettingsModelProfile()
        {
            CreateMap<GameDto, GameSettingsModel>()
              .ForMember(dest => dest.MaxPlayersCount, opt => opt.MapFrom(src => src.MaxPlayersCount))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.LocalDateTime))
              .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.LocalDateTime))
              .ForMember(dest => dest.Gamemode, opt => opt.MapFrom(src => src.Gamemode));

            CreateMap<DetailGameDto, GameSettingsModel>()
            .ForMember(dest => dest.MaxPlayersCount, opt => opt.MapFrom(src => src.MaxPlayersCount))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.LocalDateTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.LocalDateTime))
            .ForMember(dest => dest.Gamemode, opt => opt.MapFrom(src => src.Gamemode));


            CreateMap<Game, GameSettingsModel>()
            .ForMember(dest => dest.MaxPlayersCount, opt => opt.MapFrom(src => src.MaxPlayersCount))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.LocalDateTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.LocalDateTime))
            .ForMember(dest => dest.Gamemode, opt => opt.MapFrom(src => src.Gamemode));
        }
    }
}
