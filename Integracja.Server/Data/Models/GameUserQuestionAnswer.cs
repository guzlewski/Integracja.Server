namespace Integracja.Server.Data.Models
{
    public class GameUserQuestionAnswer
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public int AnswerId { get; set; }

        public Answer Answer { get; set; }

        public GameUser GameUser { get; set; }

        public GameQuestion GameQuestion { get; set; }

        public GameUserQuestion GameUserQuestion { get; set; }
    }
}
