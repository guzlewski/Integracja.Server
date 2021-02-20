using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.DTO
{
    public class GameAdd
    {
        [Required]
        public string Name { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        [Required]
        public DateTimeOffset EndTime { get; set; }
        [Required]
        public int MaxPlayers { get; set; }
        [Required]
        public int QuestionsCount { get; set; }
        [Required]
        public int GamemodeId { get; set; }
        [Required]
        public ICollection<GameQuestionAdd> QuestionPool { get; set; }
        public ICollection<GameUserAdd> InvitedUsers { get; set; }
    }
}
