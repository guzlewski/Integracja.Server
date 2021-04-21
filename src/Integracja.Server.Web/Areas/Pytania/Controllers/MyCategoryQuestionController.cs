using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Controllers
{
    [Area("Pytania")]
    public class MyCategoryQuestionController : HomeQuestionController
    {
        public static new string Name { get => "MyCategoryQuestion"; }
        public MyCategoryQuestionController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public override IActionResult Index(int categoryId)
        {
            return IndexResult("Index",MyCategoryController.Name, new { categoryId });
        }

        public override Task<IActionResult> GotoHome(int? categoryId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", MyCategoryController.Name, new { categoryId = categoryId }));
        }
    }
}
