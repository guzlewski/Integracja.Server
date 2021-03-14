using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Models.Home
{
    public interface IHomeActions
    {
        public const string NameOfGotoGameCreate = nameof(GotoGameCreate);
        public const string NameOfGotoGameRead = nameof(GotoGameRead);
        public const string NameOfGotoGameUpdate = nameof(GotoGameUpdate);
        public const string NameOfGotoGameDelete = nameof(GotoGameDelete);

        Task<IActionResult> GotoGameCreate();
        Task<IActionResult> GotoGameRead();
        Task<IActionResult> GotoGameUpdate();
        Task<IActionResult> GotoGameDelete();
    }
}
