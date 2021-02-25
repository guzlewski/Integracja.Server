using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class CreateGameDto
    {
        [Required]
        public int? GamemodeId { get; set; }
        [Required]
        public bool? RandomizeQuestionOrder { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? QuestionsCount { get; set; }
        [Required]
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public int? MaxPlayers { get; set; }
        [Required]
        public ICollection<CreateGameQuestionDto> QuestionPool { get; set; }
        public ICollection<CreateGameUserDto> InvitedUsers { get; set; }
    }
}
