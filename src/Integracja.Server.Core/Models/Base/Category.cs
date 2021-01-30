using System;
using System.Collections.Generic;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Interfaces;

namespace Integracja.Server.Core.Models.Base
{
    public class Category : IEntity, ISoftDeleteable, IHideable
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public byte[] Timestamp { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }
        public int QuestionsCount { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}