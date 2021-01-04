using System;
using System.Collections.Generic;

namespace Integracja.Server.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public bool IsDeleted { get; set; }

        public int QuestionsCount { get; set; }

        public byte[] RowVersion { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
