using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.DodajPytania.Models.Question
{
    public interface IQuestionActions : IQuestionPartialActions
    {
        public const string NameOfQuestionRead = nameof(QuestionRead);
        public const string NameOfQuestionDelete = nameof(QuestionDelete);
        public const string NameOfQuestionCreateStep1 = nameof(QuestionCreateStep1);
        public const string NameOfQuestionCreateStep2 = nameof(QuestionCreateStep2);

        Task<IActionResult> QuestionCreateStep1();
        Task<IActionResult> QuestionCreateStep2(int categoryId);
        Task<IActionResult> QuestionRead(int? id, bool? allowEdit );
        Task<IActionResult> QuestionDelete(int? id);
    }
}
