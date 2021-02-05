using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.DTO
{
    public class CategoryAdd
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        public IEnumerable<QuestionAdd> Questions { get; set; }
    }
}
