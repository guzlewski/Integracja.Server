using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class EditAnswerDto
    {
        [Required]
        public bool? IsCorrect { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
