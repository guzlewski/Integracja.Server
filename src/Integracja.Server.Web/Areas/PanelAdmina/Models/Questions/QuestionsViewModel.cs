using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Questions
{
    public class QuestionsViewModel
    {
        public List<QuestionGetAll> Questions { get; set; }
        public QuestionPartialViewModel QuestionViewModel { get; set; }

        public QuestionsViewModel() : base()
        {
            Questions = new List<QuestionGetAll>();
            QuestionViewModel = new QuestionPartialViewModel(Web.Models.Shared.Enums.ViewMode.Updating);
        }

        public const string ModalId = "ModalId";
        public const string ModalContentId = "ModalContentId";

        public static class ActionNames
        {
            public const string QuestionRead = nameof(IActions.QuestionRead);
            public const string QuestionReadToModal = nameof(IActions.QuestionReadToModal);
            public const string QuestionDelete = nameof(IActions.QuestionDelete);
        }

        public interface IActions : IQuestionPartialActions
        {
            Task<IActionResult> QuestionReadToModal(int? id);
            Task<IActionResult> QuestionRead(int? id);
            Task<IActionResult> QuestionDelete(int? id);
        }
    }
}
