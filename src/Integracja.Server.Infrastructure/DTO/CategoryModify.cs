using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.DTO
{
    public class CategoryModify
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsPublic { get; set; }
    }
}
