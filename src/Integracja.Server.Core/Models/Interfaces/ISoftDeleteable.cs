namespace Integracja.Server.Core.Models.Interfaces
{
    public interface ISoftDeleteable
    {
        public bool IsDeleted { get; set; }
    }
}
