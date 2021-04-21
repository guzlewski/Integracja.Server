using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Gamemode
{
    public class GamemodeModel
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Musisz podać nazwę trybu")]
        public string Name { get; set; }
        public int? TimeForFullQuiz { get; set; }
        public int? TimeForOneQuestion { get; set; }
        public int? NumberOfLives { get; set; }
    }
}
