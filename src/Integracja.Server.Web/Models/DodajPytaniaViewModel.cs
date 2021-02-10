using Integracja.Server.Infrastructure.DTO;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models
{
    public class DodajPytaniaViewModel
    {
        public IEnumerable<CategoryGet> Categories { get; set; }
        public QuestionAdd NewQuestion { get; set; }
        public IEnumerable<AnswerDto> NewQuestionAnswers { get; set; }
        public CategoryAdd NewCategory { get; set; }
    }
}
