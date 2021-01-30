using System.Collections.Generic;
using Integracja.Server.Core.Models.Identity;

namespace Integracja.Server.Core.Models.Base
{
    public class Category : SoftDeleteableEntity
    {
        public string Name { get; set; }
        public int QuestionsCount { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}