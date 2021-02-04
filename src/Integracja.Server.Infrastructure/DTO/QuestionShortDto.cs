﻿using System.ComponentModel.DataAnnotations;
using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.DTO
{
    public class QuestionShortDto
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        [Required]
        public string Content { get; set; }
        public int AnswersCount { get; set; }
        public int CorrectAnswersCount { get; set; }
        public float PositivePoints { get; set; }
        public float NegativePoints { get; set; }
        public QuestionScoring QuestionScoring { get; set; }
    }
}