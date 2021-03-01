using System;
using System.Collections.Generic;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Interfaces;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Models.Base
{
    public class Game : IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public int RowVersion { get; set; }

        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public GameState GameState { get; set; }
        public int? MaxPlayersCount { get; set; }

        public int GamemodeId { get; set; }
        public Gamemode Gamemode { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<GameQuestion> Questions { get; set; }
        public ICollection<GameUser> GameUsers { get; set; }
        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }
        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswer { get; set; }
    }
}