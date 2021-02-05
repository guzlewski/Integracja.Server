using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.DTO
{
    public class AnswerDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
    }
}
