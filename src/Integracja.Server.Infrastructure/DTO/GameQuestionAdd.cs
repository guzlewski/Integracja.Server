using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.DTO
{
    public class GameQuestionAdd
    {
        [Required]
        public int QuestionId { get; set; }
        public float? PositivePointsOverride { get; set; }
        public float? NegativePointsOverride { get; set; }
    }
}
