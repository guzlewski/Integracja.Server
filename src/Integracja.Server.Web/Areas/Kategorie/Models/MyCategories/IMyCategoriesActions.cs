using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Integracja.Server.Web.Areas.Pytania.Models.Shared;

namespace Integracja.Server.Web.Areas.Kategorie.Models.MyCategories
{
    public interface IMyCategoriesActions : ICategoryTableActions, ICategoryFormActions, IHomeNav
    {
    }
}
