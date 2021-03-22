using Integracja.Server.Core.Enums;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Mapper;
using Integracja.Server.Web.Models.Shared.Question;
using System;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameModel
    {
        public int Id { get; set; }
        public GameState GameState { get; set; }

        public GameSettingsModel Settings { get; set; }

        public ICollection<QuestionModel> QuestionPool { get; set; }

    }
}
