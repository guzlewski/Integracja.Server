using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Kategorie.Models.MyCategories
{
    public interface IMyCategoriesActions : ICategoryTableActions, ICategoryFormActions
    {
    }
}
