namespace Ordering.Domain.Abstractions
{
    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    public interface IEntity<TId> : IEntity
    {
        TId Id { get; }
    }
}
