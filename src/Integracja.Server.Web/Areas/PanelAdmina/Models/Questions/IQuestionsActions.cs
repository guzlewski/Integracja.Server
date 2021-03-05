using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Questions
{
    public interface IQuestionsActions : IQuestionPartialActions
    {
        public const string NameOfQuestionRead = nameof(QuestionRead);
        public const string NameOfQuestionReadToModal = nameof(QuestionReadToModal);
        public const string NameOfQuestionDelete = nameof(QuestionDelete);

        Task<IActionResult> QuestionReadToModal(int? id);
        Task<IActionResult> QuestionRead(int? id);
        Task<IActionResult> QuestionDelete(int? id);
    }
}
