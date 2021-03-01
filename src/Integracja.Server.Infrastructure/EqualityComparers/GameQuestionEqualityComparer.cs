using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Infrastructure.EqualityComparers
{
    class GameQuestionEqualityComparer : IEqualityComparer<GameQuestion>
    {
        public bool Equals(GameQuestion x, GameQuestion y)
        {
            return x.GameId == y.GameId &&
                x.QuestionId == y.QuestionId;
        }

        public int GetHashCode([DisallowNull] GameQuestion obj)
        {
            return HashCode.Combine(obj.GameId, obj.QuestionId);
        }
    }
}
