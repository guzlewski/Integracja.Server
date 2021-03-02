using Integracja.Server.Core.Enums;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.Shared.Answer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Question
{
    public class QuestionModel
    {
        [Required(ErrorMessage = "Musisz podać treść pytania")]
        [MinLength(10)]
        public string Content { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public int PositivePoints { get; set; }
        public int NegativePoints { get; set; }
        public QuestionScoring Scoring { get; set; }
        public int? CategoryId { get; set; }

        public const int DefaultAnswerCount = 4;

        // taki domyślny konstruktor jest konieczny bo asp.net core nie ogarnia z domyślnym parametrem
        public QuestionModel() : this(DefaultAnswerCount)
        { 
        }

        public QuestionModel(int answersCount = DefaultAnswerCount)
        {
            Answers = new List<AnswerModel>();
            for (int i = 0; i < answersCount; ++i)
                Answers.Add(new AnswerModel());
        }

        public void AddAnswer()
        {
            this.Answers.Add(new AnswerModel());
        }

        public void RemoveAnswer()
        {
            if (this.Answers.Count > 2)
                this.Answers.RemoveAt(this.Answers.Count - 1);
        }

        public QuestionAdd ToQuestionAdd()
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            return mapper.Map<QuestionAdd>(this);
        }

        static public List<QuestionModel> ConvertToList( IEnumerable<QuestionGetAll> dtoList )
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            List<QuestionModel> resultList = new List<QuestionModel>();
            foreach (var dtoQuestion in dtoList)
                resultList.Add(mapper.Map<QuestionModel>(dtoQuestion));
            return resultList;
        }

        public static explicit operator QuestionModel(QuestionGet v)
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            return mapper.Map<QuestionModel>(v);
        }
    }
}
