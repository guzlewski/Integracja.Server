using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Gamemode;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class GamemodeModelProfile : Profile
    {
        public GamemodeModelProfile()
        {

            CreateMap<DetailGamemodeDto, GamemodeModel>();

            CreateMap<GamemodeDto, GamemodeModel>();

            CreateMap<GamemodeModel, CreateGamemodeDto>();

            CreateMap<GamemodeModel, EditGamemodeDto>();


            CreateMap<Gamemode, GamemodeModel>();

            CreateMap<GamemodeModel, Gamemode>();
        }
    }
}
