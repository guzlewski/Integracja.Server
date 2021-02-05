using System.Collections.Generic;

namespace Integracja.Server.Infrastructure.DTO
{
    public class CategoryGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public IEnumerable<QuestionGetAll> Questions { get; set; }
        public int OwnerId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
