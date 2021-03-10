using Integracja.Server.Web.Models.Shared.Enums;

namespace Integracja.Server.Web.Models.Shared.Gamemode
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
