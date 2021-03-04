using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.DodajPytania.Models
{
    public class HomeViewModel : PageModel
    {
        public List<CategoryModel> Categories { get; set; }
        public CategoryModel Category { get; set; }

        // widok używa częściowego widoku _Question.cshtml więc załączam
        public QuestionViewModel QuestionViewModel { get; set; }

        public HomeViewModel() : base()
        {
            Categories = new List<CategoryModel>();
            Category = new CategoryModel();
            QuestionViewModel = new QuestionViewModel( allowUpdate: false, allowEdit: true );
        }

        // id używane w .cshtml
        public const string CategoryCreateFormId = "category_form";
        public static string CategoryReadFormId( int id ) => ("category_form_"+id);

        // używane do połączenia akcji od .cshtml do kontrolera
        public static class ActionNames
        {
            public const string CategoryCreate = nameof(IActions.CategoryCreate);
            public const string CategoryRead = nameof(IActions.CategoryRead);
            public const string SaveQuestionForm = nameof(IActions.SaveQuestionForm);
        }

        // do implementacji w kontrolerze
        // gdyby były problemy z wiązaniem w kontrolerze
        // to uzupełnić argumenty o atrybut [Bind(Prefix=...)]
        public interface IActions : QuestionViewModel.IActions
        {   
            Task<IActionResult> CategoryRead(
            [Bind(Prefix = nameof(Category))] CategoryModel category);
            Task<IActionResult> CategoryCreate(
            [Bind(Prefix = nameof(Category))] CategoryModel category);
            void SaveQuestionForm(
            [Bind(Prefix = nameof(QuestionViewModel.Question))] QuestionModel question);
        }
    }
}
