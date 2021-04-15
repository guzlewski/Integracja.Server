using System;
using System.Collections.Generic;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Integracja.Server.Web.Models.Shared.Time;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameSettingsModel
    {
        public string Name { get; set; }

        public GamemodeModel Gamemode { get; set; } = new GamemodeModel();

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DurationString
        {
            get
            {
                return TimeHelper.ReadableTimeSpan(EndTime - StartTime);
            }
        }

        public bool RandomizeQuestionOrder { get; set; }

        public int MaxPlayersCount { get; set; }
        public ICollection<CreateGameUserDto> InvitedUsers { get; set; }

    }
}
