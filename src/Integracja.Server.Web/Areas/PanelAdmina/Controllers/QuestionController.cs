using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.PanelAdmina.Controllers
{
    [Area("PanelAdmina")]
    public class QuestionController : Pytania.Controllers.QuestionController
    {
        public QuestionController(UserManager<User> userManager, ApplicationDbContext dbContext) : base(userManager, dbContext)
        {
        }
    }
}
