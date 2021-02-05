using System;

namespace Integracja.Server.Core.Models.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public int RowVersion { get; set; }
    }
}
