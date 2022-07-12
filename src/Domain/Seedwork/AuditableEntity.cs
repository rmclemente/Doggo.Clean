namespace Domain.Seedwork
{
    public abstract class AuditableEntity : Entity
    {
        public DateTimeOffset? CreatedAt { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTimeOffset? LastUpdateAt { get; protected set; }
        public string LastUpdateBy { get; protected set; }

        public AuditableEntity() { }
        public AuditableEntity(Guid externalId) : base(externalId) { }
    }
}
