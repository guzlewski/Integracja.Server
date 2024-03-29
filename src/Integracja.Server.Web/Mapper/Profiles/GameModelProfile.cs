﻿using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Game;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class GameModelProfile : Profile
    {
        public GameModelProfile()
        {
            CreateMap<GameModel, CreateGameDto>()
            .ForMember(dest => dest.MaxPlayers, opt => opt.MapFrom(src => src.Settings.MaxPlayersCount))
            .ForMember(dest => dest.RandomizeQuestionOrder, opt => opt.MapFrom(src => src.Settings.RandomizeQuestionOrder))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Settings.GameName))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Settings.StartDateTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Settings.EndDateTime))
            .ForMember(dest => dest.GamemodeId, opt => opt.MapFrom(src => src.Settings.Gamemode.Id))
            .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.Questions.Count))
            .ForMember(dest => dest.QuestionPool, opt => opt.MapFrom(src => src.Questions));

            CreateMap<GameDto, GameModel>()
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src));

            CreateMap<DetailGameDto, GameModel>()
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src));


            CreateMap<Game, GameModel>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src)) // z Game mapuję do GameSettings
            ;
        }
    }
}
