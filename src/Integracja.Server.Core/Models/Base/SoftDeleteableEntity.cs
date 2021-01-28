namespace Integracja.Server.Core.Models.Base
{
    public class SoftDeleteableEntity : Entity
    {
        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; }

        public SoftDeleteableEntity() : base()
        {
            IsDeleted = false;
        }
    }
}
