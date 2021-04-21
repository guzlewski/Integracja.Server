using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    // dla przekierowań dla admina z powrotem w AdminHome zamiast Home
    public class AdminQuestionController : HomeQuestionController
    {
        public static new string Name { get => "AdminQuestion"; }
        protected override string QuestionViewName => "AdminQuestion";
        public AdminQuestionController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public override IActionResult Index(int categoryId)
        {
            return IndexResult("Index", AdminHomeController.Name);
        }
    }
}
