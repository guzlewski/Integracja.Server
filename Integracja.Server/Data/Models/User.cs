﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Integracja.Server.Data.Models
{
    public class User : IdentityUser<int>
    {
        public byte[] Picture { get; set; }

        public ICollection<Question> CreatedQuestions { get; set; }

        public ICollection<Category> CreatedCategories { get; set; }

        public ICollection<Game> CreatedGames { get; set; }

        public ICollection<Gamemode> CreatedGameModes { get; set; }

        public ICollection<GameUser> PlayedGames { get; set; }

        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }

        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}
