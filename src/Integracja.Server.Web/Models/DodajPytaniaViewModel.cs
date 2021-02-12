using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Integracja.Server.Web.Models
{
    public class DodajPytaniaViewModel : PageModel
    {
        public IEnumerable<CategoryGetAll> Categories { get; set; }
        public QuestionAdd NewQuestion { get; set; }
        public IEnumerable<AnswerDto> NewQuestionAnswers { get; set; }
        public CategoryAdd NewCategory { get; set; }

        public string TestString { get; set;  }
        public DodajPytaniaViewModel() : base()
        {
            Categories = new List<CategoryGetAll>();
            NewQuestion = new QuestionAdd();
            NewQuestionAnswers = new List<AnswerDto>();
            NewCategory = new CategoryAdd();
            TestString = "teststring";
        }
    }
}
