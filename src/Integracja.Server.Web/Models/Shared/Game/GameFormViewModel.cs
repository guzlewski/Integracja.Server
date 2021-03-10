using Integracja.Server.Web.Models.Shared.Enums;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameFormViewModel
    {
        public GameModel Game { get; set; } = new GameModel();
        public ViewMode ViewMode { get; set; } = ViewMode.Creating;

        public GameFormViewModel()
        {
        }

        public GameFormViewModel(ViewMode viewMode) => (ViewMode) = (viewMode);
    }
}
