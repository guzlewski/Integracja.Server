﻿using Integracja.Server.Core.Enums;
using Integracja.Server.Infrastructure.DTO;
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

        public QuestionModel()
        {
        }
        public QuestionModel(int answersCount)
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
    }
}
