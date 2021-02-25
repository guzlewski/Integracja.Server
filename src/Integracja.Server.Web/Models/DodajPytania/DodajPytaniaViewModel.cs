using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.DodajPytania
{
    public class DodajPytaniaViewModel : PageModel
    {
        public IEnumerable<CategoryGetAll> Categories { get; set; }
        public CategoryModel NewCategory { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }

        public const string CategoryFormId = "CategoryFormId";

        public DodajPytaniaViewModel() : base()
        {
            Categories = new List<CategoryGetAll>();
            NewCategory = new CategoryModel();
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
            Task<IActionResult> CategoryRead(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question);
            Task<IActionResult> CategoryCreate(
            [Bind(Prefix = nameof(NewCategory))] CategoryModel newCategory);

            void SaveQuestionForm(QuestionModel question);
        }
    }
}
