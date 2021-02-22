namespace Integracja.Server.Web.Models.Question
{
    public class AnswerModel
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        public AnswerModel()
        {
        }

        public AnswerModel(string content, bool isCorrect) => (Content, IsCorrect) = (content, isCorrect);
    }
}
