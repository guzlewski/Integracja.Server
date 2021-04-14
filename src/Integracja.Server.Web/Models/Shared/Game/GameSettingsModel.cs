using System;
using System.Collections.Generic;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Gamemode;

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
                var duration = EndTime - StartTime;
                int days = duration.Days;
                int hours = duration.Hours;
                int minutes = duration.Minutes;

                var output = "error: DurationString";
                if (days > 0)
                {
                    output = days + "d " + hours + "h " + minutes + "m";
                }
                else if (hours > 0)
                {
                    output = hours + "h " + minutes + "m";
                }
                else if( minutes > 0)
                {
                    output = minutes + "m";
                }
                return output;
            }
        }

        public bool RandomizeQuestionOrder { get; set; }

        public int MaxPlayersCount { get; set; }
        public ICollection<CreateGameUserDto> InvitedUsers { get; set; }

    }
}
