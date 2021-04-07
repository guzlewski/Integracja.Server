using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Gamemode;

namespace Integracja.Server.Web.Areas.TrybyGry.Models.Shared
{
    public class GamemodeFormViewModel
    {
        public GamemodeModel Gamemode { get; set; } = new GamemodeModel();
        public ViewMode ViewMode { get; set; } = ViewMode.Creating;

        public GamemodeFormViewModel()
        {
        }

        public GamemodeFormViewModel(ViewMode viewMode) => (ViewMode) = (viewMode);
    }
}
