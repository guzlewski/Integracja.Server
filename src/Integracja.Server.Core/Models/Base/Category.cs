using System;
using System.Collections.Generic;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Interfaces;

namespace Integracja.Server.Core.Models.Base
{
    public class Category : IEntity, ISoftDeleteable, IHideable
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public int RowVersion { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}