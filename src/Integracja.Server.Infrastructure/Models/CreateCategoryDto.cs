using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class CreateCategoryDto
    {
        [Required]
        public bool? IsPublic { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<CreateQuestionDto> Questions { get; set; }
    }
}
