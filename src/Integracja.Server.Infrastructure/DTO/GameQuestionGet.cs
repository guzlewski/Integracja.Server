namespace Integracja.Server.Infrastructure.DTO
{
    public class GameQuestionGet
    {
        public int Index { get; set; }
        public float? OverridePositivePoints { get; set; }
        public float? OverrideNegativePoints { get; set; }

        public int QuestionId { get; set; }
        public QuestionGet Question { get; set; }
    }
}
