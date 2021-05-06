using System;
using System.Collections.Generic;
using Integracja.Server.Core.Enums;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameModel
    {
        public int Id { get; set; }
        public GameState GameState { get; set; }

        public Guid Guid { get; set; }

        public GameSettingsModel Settings { get; set; }

        public List<QuestionModel> Questions { get; set; }

    }
}
