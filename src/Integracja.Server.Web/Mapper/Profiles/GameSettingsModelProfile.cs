using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Game;
using System;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class GameSettingsModelProfile : Profile
    {
        public GameSettingsModelProfile()
        {
            CreateMap<DateTimeOffset, DateTime>().ConvertUsing( new DateTimeTypeConverter() );

            CreateMap<GameDto, GameSettingsModel>()
              .ForMember(dest => dest.MaxPlayersCount, opt => opt.MapFrom(src => src.MaxPlayersCount))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.StartTime, opt => opt.Ignore())
              .ForMember(dest => dest.EndTime, opt => opt.Ignore())
              .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartTime.LocalDateTime))
              .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndTime.LocalDateTime))
              .ForMember(dest => dest.Gamemode, opt => opt.MapFrom(src => src.Gamemode));

            CreateMap<DetailGameDto, GameSettingsModel>()
            .ForMember(dest => dest.MaxPlayersCount, opt => opt.MapFrom(src => src.MaxPlayersCount))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StartTime, opt => opt.Ignore())
            .ForMember(dest => dest.EndTime, opt => opt.Ignore())
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartTime.LocalDateTime))
            .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndTime.LocalDateTime))
            .ForMember(dest => dest.Gamemode, opt => opt.MapFrom(src => src.Gamemode));


            CreateMap<Game, GameSettingsModel>()
            .ForMember(dest => dest.MaxPlayersCount, opt => opt.MapFrom(src => src.MaxPlayersCount))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StartTime, opt => opt.Ignore())
            .ForMember(dest => dest.EndTime, opt => opt.Ignore())
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartTime.LocalDateTime))
            .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndTime.LocalDateTime))
            .ForMember(dest => dest.Gamemode, opt => opt.MapFrom(src => src.Gamemode));
        }
    }

    public class DateTimeTypeConverter : ITypeConverter<DateTimeOffset, DateTime>
    {
        public DateTime Convert(DateTimeOffset source, DateTime destination, ResolutionContext context)
        {
            return source.DateTime;
        }
    }
}
