using System.Collections.Generic;

namespace Integracja.Server.Infrastructure.Models
{
    public class DetailCategoryDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }
}
