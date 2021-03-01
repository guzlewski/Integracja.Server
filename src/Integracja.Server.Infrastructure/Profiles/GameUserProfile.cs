using AutoMapper;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class GameUserProfile : Profile
    {
        public GameUserProfile()
        {
            CreateMap<GameUser, GameUserDto>()
               .ForMember(
                   gameUserDto => gameUserDto.Username,
                   opt => opt.MapFrom(gameUser => gameUser.User.UserName));

            CreateMap<CreateGameUserDto, GameUser>();
        }
    }
}
