using System.Linq;
using AutoMapper;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDto>()
                .ForMember(
                    gameDto => gameDto.PlayersCount,
                    opt => opt.MapFrom(game => game.GameUsers.Where(gu => gu.GameUserState != GameUserState.Left).Count()));

            CreateMap<Game, DetailGameDto>()
                .ForMember(
                    gameDto => gameDto.Players,
                    opt => opt.MapFrom(game => game.GameUsers.Where(gu => gu.GameUserState != GameUserState.Left)));

            CreateMap<CreateGameDto, Game>()
                .ForMember(
                    game => game.Questions,
                    opt => opt.MapFrom(gameDto => gameDto.QuestionPool))
                .ForMember(
                    game => game.GameUsers,
                    opt => opt.MapFrom(gameDto => gameDto.InvitedUsers))
                 .ForMember(
                    game => game.MaxPlayersCount,
                    opt => opt.MapFrom(gameDto => gameDto.MaxPlayers));

            CreateMap<EditGameDto, Game>();
        }
    }
}
