using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.PanelAdmina.Models.Questions
{
    public interface IQuestionsActions
    {
        public const string NameOfGotoQuestionRead = nameof(GotoQuestionRead);
        public const string NameOfGotoQuestionDelete = nameof(GotoQuestionDelete);
        public const string NameOfGotoQuestionUpdate = nameof(GotoQuestionUpdate);
        public const string NameOfGotoQuestionCreate = nameof(GotoQuestionCreate);

        Task<IActionResult> GotoQuestionCreate(int? id);
        Task<IActionResult> GotoQuestionRead(int? id);
        Task<IActionResult> GotoQuestionDelete(int? id);
        Task<IActionResult> GotoQuestionUpdate(int? id);
    }
}
