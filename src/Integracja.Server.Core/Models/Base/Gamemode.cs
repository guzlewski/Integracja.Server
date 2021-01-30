using System.Collections.Generic;
using Integracja.Server.Core.Models.Identity;

namespace Integracja.Server.Core.Models.Base
{
    public class Gamemode : SoftDeleteableEntity
    {
        public string Name { get; set; }
        public int? TimeForOneQuestion { get; set; }
        public int? TimeForFullQuiz { get; set; }
        public int? NumberOfLives { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}