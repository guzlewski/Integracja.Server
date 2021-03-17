namespace Integracja.Server.Infrastructure.Models
{
    public class GameQuestionDto
    {
        public int Index { get; set; }
        public float? OverridePositivePoints { get; set; }
        public float? OverrideNegativePoints { get; set; }
        public DetailQuestionDto<AnswerDto> Question { get; set; }
    }
}
