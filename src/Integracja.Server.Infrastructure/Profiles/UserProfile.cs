using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, DetailUserDto>().BeforeMap((user, userDto, resContext) =>
            {
                var token = resContext.Items["token"] as JwtSecurityToken;

                userDto.ValidTo = token.ValidTo;
                userDto.Token = token.RawData;
            });
        }
    }
}
