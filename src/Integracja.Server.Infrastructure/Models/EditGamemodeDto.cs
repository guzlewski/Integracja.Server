using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class EditGamemodeDto
    {
        [Required]
        public bool? IsPublic { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, Range(1, 31536000)]
        public int? TimeForFullQuiz { get; set; }
        [Range(0, 31536000)]
        public int? TimeForOneQuestion { get; set; }
        [Range(0, 100)]
        public int? NumberOfLives { get; set; }
    }
}
