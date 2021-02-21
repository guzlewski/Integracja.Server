using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Infrastructure.DTO
{
   public class GameUserQuestionGet
    {
        public float? QuestionScore { get; set; }
        public bool IsAnswered { get; set; }
        public DateTimeOffset? QuestionDownloadTime { get; set; }
        public DateTimeOffset? QuestionAnswerTime { get; set; }

        public QuestionGet Question { get; set; }
    }
}
