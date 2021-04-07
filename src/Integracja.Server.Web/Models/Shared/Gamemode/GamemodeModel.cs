using Integracja.Server.Infrastructure.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Gamemode
{
    public class GamemodeModel
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public int? TimeForFullQuiz { get; set; }
        public int? TimeForOneQuestion { get; set; }
        public int? NumberOfLives { get; set; }
    }
}
