using Integracja.Server.Web.Areas.Kategorie.Models.CategorySelect;
using Integracja.Server.Web.Models.Shared.Category;

namespace Integracja.Server.Web.Areas.Kategorie.Models.CategoryForQuestion
{
    public class CategoryForQuestionViewModel
    {
        public CategorySelectViewModel CategorySelectModel { get; set; } = new CategorySelectViewModel();

        public CategoryFormViewModel CategoryFormModel { get; set; } = new CategoryFormViewModel();

        public CategoryForQuestionViewModel() : base()
        {
        }
    }
}
