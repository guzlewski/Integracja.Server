using System;
using System.Collections.Generic;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Interfaces;
using Integracja.Server.Core.Models.Joins;
using Microsoft.AspNetCore.Identity;

namespace Integracja.Server.Core.Models.Identity
{
    public class User : IdentityUser<int>, ISoftDeleteable
    {
        public bool IsDeleted { get; set; }
        public Guid? SessionGuid { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfileThumbnail { get; set; }

        public ICollection<Category> CreatedCategories { get; set; }
        public ICollection<Question> CreatedQuestions { get; set; }
        public ICollection<Gamemode> CreatedGamemodes { get; set; }
        public ICollection<Game> CreatedGames { get; set; }

        public ICollection<GameUser> GameUsers { get; set; }
        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }
        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}