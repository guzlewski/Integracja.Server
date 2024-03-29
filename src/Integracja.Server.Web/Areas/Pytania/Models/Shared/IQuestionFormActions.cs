﻿using System.Threading.Tasks;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Pytania.Models.Shared
{
    public interface IQuestionFormActions
    {
        Task<IActionResult> AddAnswerField(
        QuestionModel question);

        Task<IActionResult> RemoveAnswerField(
        QuestionModel question);

        Task<IActionResult> QuestionCreate(
        QuestionModel question);

        Task<IActionResult> QuestionUpdate(
        QuestionModel question);
    }
}
