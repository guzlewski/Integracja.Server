using Integracja.Server.Core.Enums;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Question;
using System;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameModel
    {
        public int Id { get; set; }
        public GameState GameState { get; set; }

        public GameSettingsModel Settings { get; set; }

        public ICollection<QuestionModel> QuestionPool { get; set; }

        public T MapTo<T>() => Mappers.WebAutoMapper.Initialize().Map<T>(this);

        public static List<GameModel> MapToList<T>( IEnumerable<T> dtoList )
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            List<GameModel> resultList = new List<GameModel>();
            foreach (var gamemode in dtoList)
                resultList.Add(mapper.Map<GameModel>(gamemode));
            return resultList;
        }
    }
}
