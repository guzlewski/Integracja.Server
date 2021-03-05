using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Question
{

    public class QuestionPartialViewModel : PageModel
    {
        public string Controller { get; private set; }
        public bool AllowEdit { get; private set; }
        public bool AllowUpdate { get; private set; }

        [BindProperty]
        public QuestionModel Question { get; set; }

        // AllowEdit miałby służyć wyświetlaniu widoku w wersji tylko do przeglądu dla użytkownika np.: dla strony przeglądania pytań
        // nie wiem do końca jeszcze czy takie coś będzie, w jakiej postaci itd. więc na razie to martwy kod
        public QuestionPartialViewModel( bool allowUpdate = false, bool allowEdit = true, string controllerName = "") : base()
        {
            Controller = controllerName;
            AllowEdit = allowEdit;
            if (AllowEdit == false) // jeśli nie można edytować nie można updatować
                AllowUpdate = false;
            else AllowUpdate = allowUpdate;

            Question = new QuestionModel();
        }
        
        public static class Ids
        {
            public const string Form = "question_form";
        }
    }
}
