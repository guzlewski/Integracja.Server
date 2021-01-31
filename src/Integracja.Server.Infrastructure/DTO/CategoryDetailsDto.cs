using System.Collections.Generic;

namespace Integracja.Server.Infrastructure.DTO
{
    public class CategoryDetailsDto : CategoryDto
    {
        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
