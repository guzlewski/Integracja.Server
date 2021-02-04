using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.DTO
{
    public class AnswerDto
    {
        //public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
