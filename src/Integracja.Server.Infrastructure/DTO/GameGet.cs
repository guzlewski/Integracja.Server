using System;
using System.Collections.Generic;
using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.DTO
{
    public class GameGet
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public GameState GameState { get; set; }
        public int MaxPlayersCount { get; set; }
        public int QuestionsCount { get; set; }

        public GamemodeGet Gamemode { get; set; }
        public ICollection<string> Players { get; set; }
    }
}
