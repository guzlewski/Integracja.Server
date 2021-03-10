using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Gry.Controllers
{
    [Area("Gry")]
    public class GameController : ApplicationController
    {
        public static new string Name { get => "Game"; }

        public GameController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

    }
}