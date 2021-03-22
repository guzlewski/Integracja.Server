using Integracja.Server.Core.Enums;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Mappers;
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

        public static explicit operator GameModel(DetailGameDto v)
        {
            return WebAutoMapper.Initialize().Map<GameModel>(v);
        }
    }
}
