using Integracja.Server.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameSettingsModel
    {
        public int? GamemodeId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public bool RandomizeQuestionOrder { get; set; }

        public int MaxPlayersCount { get; set; }
        public ICollection<CreateGameUserDto> InvitedUsers { get; set; }

        public static List<GameModel> MapToList<T>(IEnumerable<T> dtoList)
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            List<GameModel> resultList = new List<GameModel>();
            foreach (var gamemode in dtoList)
            {
                var settings = mapper.Map<GameSettingsModel>(gamemode);
                var game = new GameModel();
                game.Settings = settings;
                resultList.Add(game);
            }
            return resultList;
        }
    }
}
