using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Models.OwnedCategories
{
    public interface IOwnedCategoriesActions : ICategoryTableActions, ICategoryFormActions
    {
    }
}
