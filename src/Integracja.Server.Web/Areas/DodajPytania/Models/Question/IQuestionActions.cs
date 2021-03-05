using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.DodajPytania.Models.Question
{
    public interface IQuestionActions : IQuestionPartialActions
    {
        public const string NameOfQuestionReadView = nameof(QuestionReadView);
        public const string NameOfQuestionDelete = nameof(QuestionDelete);
        public const string NameOfQuestionCreateViewStep1 = nameof(QuestionCreateViewStep1);
        public const string NameOfQuestionCreateViewStep2 = nameof(QuestionCreateViewStep2);
        public const string NameOfQuestionUpdateView = nameof(QuestionUpdateView);

        Task<IActionResult> QuestionCreateViewStep1();
        Task<IActionResult> QuestionCreateViewStep2(int categoryId);
        Task<IActionResult> QuestionReadView(int? id );
        Task<IActionResult> QuestionUpdateView(int? id);
        Task<IActionResult> QuestionDelete(int? id);
    }
}
