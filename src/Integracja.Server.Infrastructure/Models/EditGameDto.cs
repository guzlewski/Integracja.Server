using System;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class EditGameDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTimeOffset? StartTime { get; set; }
        [Required]
        public DateTimeOffset? EndTime { get; set; }
        [Required]
        public int? MaxPlayersCount { get; set; }
    }
}
