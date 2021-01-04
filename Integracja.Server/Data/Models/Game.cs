using System;
using System.Collections.Generic;

namespace Integracja.Server.Data.Models
{
    public enum GameState
    {
        Normal,
        Cancelled,
        Deleted
    }

    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public GameState GameState { get; set; }

        public Guid PublicId { get; set; }

        public int PlayersCount { get; set; }

        public int MaxPlayersCount { get; set; }

        public int QuestionsCount { get; set; }

        public byte[] RowVersion { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }

        public int GamemodeId { get; set; }

        public Gamemode GameMode { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }

        public ICollection<GameUser> Players { get; set; }

        public ICollection<GameQuestion> Questions { get; set; }

        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }

        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswer { get; set; }
    }
}
