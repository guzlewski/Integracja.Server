using Integracja.Server.Core.Enums;
using Integracja.Server.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameState GameState { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public int? GamemodeId { get; set; }

        public int QuestionsCount { get; set; }
        public bool? RandomizeQuestionOrder { get; set; }
        public ICollection<CreateGameQuestionDto> QuestionPool { get; set; }

        public int MaxPlayersCount { get; set; }
        public ICollection<CreateGameUserDto> InvitedUsers { get; set; }


        public static explicit operator GameModel(DetailGamemodeDto v) => Mappers.WebAutoMapper.Initialize().Map<GameModel>(v);

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
