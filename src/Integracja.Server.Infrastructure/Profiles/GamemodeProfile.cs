using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class GamemodeProfile : Profile
    {
        public GamemodeProfile()
        {
            CreateMap<Gamemode, GamemodeDto>();

            CreateMap<Gamemode, DetailGamemodeDto>();

            CreateMap<CreateGamemodeDto, Gamemode>();

            CreateMap<EditGamemodeDto, Gamemode>();
        }
    }
}
