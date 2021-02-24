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
        }
        public interface IActions
        {
            // QuestionModel jest tutaj po to by umożliwić zapisanie formy przy zmianie/dodaniu kategorii
            Task<IActionResult> CategoryCreate(
            [Bind(Prefix = nameof(NewCategory))] CategoryAdd newCategory);

            Task<IActionResult> CategoryRead(
            int? id,
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question);
        }
    }
}
