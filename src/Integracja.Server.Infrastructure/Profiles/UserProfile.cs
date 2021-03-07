using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().BeforeMap((user, userDto, resContext) =>
            {
                userDto.Authorization = resContext.Items["tokenDto"] as TokenDto;
            });
        }
    }
}
