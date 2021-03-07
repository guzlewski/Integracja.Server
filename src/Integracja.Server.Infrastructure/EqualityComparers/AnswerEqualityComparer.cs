using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Infrastructure.EqualityComparers
{
    public class AnswerEqualityComparer : IEqualityComparer<Answer>
    {
        public bool Equals(Answer x, Answer y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Answer obj)
        {
            return obj.Id;
        }
    }
}
