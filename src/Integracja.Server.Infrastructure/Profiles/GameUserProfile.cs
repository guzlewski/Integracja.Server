using AutoMapper;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class GameUserProfile : Profile
    {
        public GameUserProfile()
        {
            CreateMap<GameUser, UserDto>()
               .ForMember(
                   userDto => userDto.Username,
                   opt => opt.MapFrom(gameUser => gameUser.User.UserName))
               .ForMember(
                   userDto => userDto.ProfileThumbnail,
                   opt => opt.MapFrom(gameUser => gameUser.User.ProfileThumbnail));

            CreateMap<GameUser, GameUserDto<GameDto>>();

            CreateMap<GameUser, GameUserDto<DetailGameDto>>();

            CreateMap<GameUser, DetailGameUserDto>()
               .ForMember(
                  detailGameUserDto => detailGameUserDto.PlayerScores,
                  opt => opt.MapFrom(gameUser => gameUser.Game.GameUsers));

            CreateMap<GameUser, GameUserScoreDto>()
                .ForMember(
                   gameUserScoreDto => gameUserScoreDto.Username,
                   opt => opt.MapFrom(gameUser => gameUser.User.UserName))
               .ForMember(
                   gameUserScoreDto => gameUserScoreDto.ProfileThumbnail,
                   opt => opt.MapFrom(gameUser => gameUser.User.ProfileThumbnail));

            CreateMap<CreateGameUserDto, GameUser>();
        }
    }
}
