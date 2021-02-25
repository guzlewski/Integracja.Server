using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class CreateGameQuestionDto
    {
        [Required]
        public int QuestionId { get; set; }
        public float? PositivePointsOverride { get; set; }
        public float? NegativePointsOverride { get; set; }
    }
}
