using Integracja.Server.Core.Enums;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.Question
{
    /* służy reprezentacji formy w widoku i w kontrolerze */
    public class QuestionModel
    {
        public string Content { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public int PositivePoints { get; set; }
        public int NegativePoints { get; set; }
        public QuestionScoring Scoring { get; set; }
        public int? CategoryId { get; set; }

        public QuestionModel()
        {
        }
        public QuestionModel(int answersCount)
        {
            Answers = new List<AnswerModel>();
            for (int i = 0; i < answersCount; ++i)
                Answers.Add(new AnswerModel());
        }
    }
}
