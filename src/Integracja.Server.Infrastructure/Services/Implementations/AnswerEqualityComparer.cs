using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class AnswerEqualityComparer : EqualityComparer<Answer>
    {
        public override bool Equals(Answer x, Answer y)
        {
            return x.Id == y.Id;
        }

        public override int GetHashCode([DisallowNull] Answer obj)
        {
            return obj.Id;
        }
    }
}
