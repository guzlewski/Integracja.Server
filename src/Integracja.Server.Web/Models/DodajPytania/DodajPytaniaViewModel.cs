using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models
{
    public class DodajPytaniaViewModel : PageModel
    {
        public IEnumerable<CategoryGetAll> Categories { get; set; }
        public CategoryAdd NewCategory { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }

        public DodajPytaniaViewModel() : base()
        {
            Categories = new List<CategoryGetAll>();
            NewCategory = new CategoryAdd();
            QuestionViewModel = new QuestionViewModel("Twoje pytanie", true, "DodajPytania");
        }

        public static class ActionNames 
        {
            public const string CategoryCreate = nameof(IActions.CategoryCreate);
            public const string CategoryRead = nameof(IActions.CategoryRead);
            public const string SaveQuestionForm = nameof(IActions.SaveQuestionForm);
        }
        public interface IActions
        {
            Task<IActionResult> CategoryCreate(
            [Bind(Prefix = nameof(DodajPytaniaViewModel.NewCategory))] CategoryAdd newCategory);

            Task<IActionResult> SaveQuestionForm(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question);

            Task<IActionResult> CategoryRead(int? categoryId);
        }
    }
}
