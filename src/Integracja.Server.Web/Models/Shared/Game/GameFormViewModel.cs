using Integracja.Server.Web.Models.Shared.Category;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Question;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameFormViewModel
    {
        public GameModel Game { get; set; } = new GameModel();
        public ViewMode ViewMode { get; set; } = ViewMode.Creating;

        public List<CategoryModel> Categories { get; set; }
        public List<QuestionModel> Questions { get; set; }
        public List<QuestionModel> SelectedQuestions { get; set; }
        public GameSettingsModel Settings { get; set; }

        public GameFormViewModel()
        {
        }

        public GameFormViewModel(ViewMode viewMode) => (ViewMode) = (viewMode);
    }
}
