using System;
using System.Collections.Generic;

namespace Integracja.Server.Infrastructure.Models
{
    public class DetailGameUserDto
    {
        public float? GameScore { get; set; }
        public DateTimeOffset? GameStartTime { get; set; }
        public DateTimeOffset? GameEndTime { get; set; }
        public bool GameOver { get; set; }
        public int AnsweredQuestions { get; set; }
        public int CorrectlyAnsweredQuestions { get; set; }
        public int IncorrectlyAnsweredQuestions { get; set; }
        public IEnumerable<DetailGameUserQuestionDto> GameUserQuestions { get; set; }
        public IEnumerable<GameUserScoreDto> PlayerScores { get; set; }
    }
}
