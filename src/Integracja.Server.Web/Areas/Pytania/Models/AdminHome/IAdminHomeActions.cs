using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Pytania.Models.AdminHome
{
    public interface IAdminHomeActions
    {
        Task<IActionResult> GotoQuestionCreate(int? id);
        Task<IActionResult> GotoQuestionRead(int? id);
        Task<IActionResult> GotoQuestionDelete(int? id);
        Task<IActionResult> GotoQuestionUpdate(int? id);
    }
}
