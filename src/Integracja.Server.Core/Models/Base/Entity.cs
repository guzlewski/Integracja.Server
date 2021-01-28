using System;

namespace Integracja.Server.Core.Models.Base
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public byte[] Timestamp { get; set; }

        public Entity()
        {
            CreatedDate = DateTimeOffset.Now;
        }
    }
}
