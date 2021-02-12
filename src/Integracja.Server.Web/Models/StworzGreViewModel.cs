using Integracja.Server.Infrastructure.DTO;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models
{
    public class StworzGreViewModel
    {
        public IEnumerable<QuestionGetAll> Questions { get; set; }
        public IEnumerable<QuestionGetAll> PickedQuestions { get; set; }
        public GamemodeAdd Gamemode { get; set; }

        //WIP
    }
}
