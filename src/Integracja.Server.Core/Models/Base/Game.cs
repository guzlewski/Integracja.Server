using System;
using System.Collections.Generic;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Models.Base
{
    public class Game : Entity
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public GameState GameState { get; set; }
        public int PlayersCount { get; set; }
        public int MaxPlayersCount { get; set; }
        public int QuestionsCount { get; set; }

        public int GamemodeId { get; set; }
        public Gamemode Gamemode { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<GameUser> Players { get; set; }
        public ICollection<GameQuestion> Questions { get; set; }
        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }
        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswer { get; set; }
    }
}