using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.DTO
{
    public class QuestionAdd
    {
        [Required]
        [Display(Name ="Treść")]
        public string Content { get; set; }
        [Required]
        [Range(0, 1000)]
        public float? PositivePoints { get; set; }
        [Range(-1000, 0)]
        public float? NegativePoints { get; set; }
        [Required]
        public QuestionScoring? QuestionScoring { get; set; }
        [Required]
        public bool? IsPublic { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        [Required]
        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}
