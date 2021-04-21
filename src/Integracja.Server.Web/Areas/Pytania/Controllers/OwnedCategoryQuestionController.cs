using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class OwnedCategoryQuestionController : HomeQuestionController
    {
        public static new string Name { get => "OwnedCategoryQuestion"; }
        public OwnedCategoryQuestionController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public override IActionResult Index(int categoryId)
        {
            return IndexResult("Index",OwnedCategoryQuestionsController.Name, new { categoryId });
        }

        public override Task<IActionResult> GotoHome(int? categoryId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", OwnedCategoryQuestionsController.Name, new { categoryId = categoryId }));
        }
    }
}
