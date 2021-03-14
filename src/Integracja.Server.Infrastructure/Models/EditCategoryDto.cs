using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class EditCategoryDto
    {
        [Required]
        public bool? IsPublic { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
