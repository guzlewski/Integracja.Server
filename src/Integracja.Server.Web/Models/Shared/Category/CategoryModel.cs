using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Category
{
    public class CategoryModel
    {
        [Required]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Podaj nazwę kategorii")]
        public string Name { get; set; }
        public int QuestionsCount { get; set; }
        public bool IsPublic { get; set; }

    }
}
