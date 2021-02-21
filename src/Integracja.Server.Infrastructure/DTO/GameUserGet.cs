﻿using System;
using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.DTO
{
    public class GameUserGet
    {
        public float? GameScore { get; set; }
        public DateTimeOffset? GameStartTime { get; set; }
        public DateTimeOffset? GameEndTime { get; set; }
        public GameUserState State { get; set; }
        public int AnsweredQuestions { get; set; }
        public int CorrectlyAnsweredQuestions { get; set; }
        public int IncorrectlyAnsweredQuestions { get; set; }

        public GameGet Game { get; set; }
    }
}
