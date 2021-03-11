using System;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameSettingsModel
    {
        public int? GamemodeId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool? RandomizeQuestionOrder { get; set; }
        public int MaxPlayersCount { get; set; }
    }
}
