using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Controllers
{
    public class ApplicationController : Controller
    {
        private ApplicationDbContext _context;
        protected ApplicationDbContext DbContext { get => _context; }

        private UserManager<User> _userManager;
        protected UserManager<User> UserManager { get => _userManager; }

        // using dependency injection
        public ApplicationController( UserManager<User> userManager, ApplicationDbContext dbContext ) : base()
        {
            _context = dbContext;
            _userManager = userManager;
        }
    }
}
