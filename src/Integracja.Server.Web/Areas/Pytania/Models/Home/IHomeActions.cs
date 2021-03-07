 using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Pytania.Models.Home
{
    public interface IHomeActions
    {
        public const string NameOfGotoQuestionCreate = nameof(GotoQuestionCreate);
        public const string NameOfGotoQuestionRead = nameof(GotoQuestionRead);
        public const string NameOfGotoQuestionUpdate = nameof(GotoQuestionUpdate);
        public const string NameOfGotoQuestionDelete = nameof(GotoQuestionDelete);

        Task<IActionResult> GotoQuestionCreate();
        Task<IActionResult> GotoQuestionRead(int? id);
        Task<IActionResult> GotoQuestionUpdate(int? id);
        Task<IActionResult> GotoQuestionDelete(int? id);
        Task<IActionResult> Index(int? id);
    }
}
