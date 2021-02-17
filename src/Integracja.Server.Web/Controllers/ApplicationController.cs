using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Mappers;
using Integracja.Server.Infrastructure.Repositories;
using Integracja.Server.Infrastructure.Services;
using Integracja.Server.Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Integracja.Server.Web.Controllers
{
    /*
     bazowa klasa dla kontrolerów naszych stron po to żeby w każdym kontrolerze było ApplicationDbContext i UserManager 
     DbContext jest potrzebny do użycia z serwisem - serwis potrzebujesz do komunikacji się z bazą danych i dodawania/pobierania rzeczy z bazy danych
     UserManager przechowuje informacje o zalogowanym uzytkowniku i tez np userid jest potrzebne do polaczenia z bazą danych przez serwis no bo mamy te prywatne kategorie itd*/
    public class ApplicationController : Controller
    {
        // using dependency injection
        public ApplicationController(UserManager<User> userManager, ApplicationDbContext dbContext) : base()
        {
            _context = dbContext;
            _userManager = userManager;
            _userId = null;
            _mapper = AutoMapperConfig.Initialize();
        }

        private ApplicationDbContext _context;
        protected ApplicationDbContext DbContext { get => _context; }

        private UserManager<User> _userManager;
        protected UserManager<User> UserManager { get => _userManager; }

        private AutoMapper.IMapper _mapper;
        protected AutoMapper.IMapper Mapper { get => _mapper; }

        private int? _userId;
        protected int UserId
        {
            get
            {
                if (!_userId.HasValue)
                    _userId = Int32.Parse(UserManager.GetUserId(User));
                return _userId.Value;
            }
        }
    }
}
