using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Integracja.Server.Core.Enums;
using Integracja.Server.Web.Models.Shared.Answer;

namespace Integracja.Server.Web.Models.Shared.Question
{
    public class QuestionModel
    {
        [Required(ErrorMessage = "Musisz podać treść pytania")]
        [Display(Name = "Treść")]
        // dodatkowo wpisanie tylko spacji przejdzie walidację jquery a nie przejdzie tej na serwerze
        [RegularExpression(@"(^[\S])(.*)", ErrorMessage = "Pytanie nie może zaczynać się od białego znaku")] 
        public string Content { get; set; }
        [Required]
        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();
        [Range(minimum: 1, maximum: 10, ErrorMessage = "Punkty muszą być w zakresie 1 do 10")]
        [Required(ErrorMessage = "Musisz podać liczbę punktów za dobrą odpowiedź")]
        public int PositivePoints { get; set; }
        [Range(minimum: -10, maximum: 0, ErrorMessage = "Punkty muszą być w zakresie -10 do 0")]
        public int NegativePoints { get; set; }
        [Required]
        public QuestionScoring Scoring { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Id { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPersisted { get => Id.HasValue; }

        public const int DefaultAnswerCount = 4;

        public QuestionModel() : this(DefaultAnswerCount)
        {
        }

        public QuestionModel(int answerCount)
        {
            for (int i = 0; i < answerCount; ++i)
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

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as QuestionModel);
        }

        public bool Equals(QuestionModel obj)
        {
            if (Object.ReferenceEquals(obj, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Id == obj.Id;
        }
    }
}
