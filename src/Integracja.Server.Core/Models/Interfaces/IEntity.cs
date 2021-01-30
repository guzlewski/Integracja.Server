using System;

namespace Integracja.Server.Core.Models.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
