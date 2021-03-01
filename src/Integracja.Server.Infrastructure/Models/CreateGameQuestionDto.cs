using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class CreateGameQuestionDto
    {
        [Required]
        public int QuestionId { get; set; }
        [Range(0, 1000)]
        public float? PositivePointsOverride { get; set; }
        [Range(-1000, 0)]
        public float? NegativePointsOverride { get; set; }
    }
}
