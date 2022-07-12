namespace Application.SeedWork;

public abstract class AuditableResponse : BaseRequestResponse
{
    public DateTime? CreatedAt { get; protected set; }
    public string CreatedBy { get; protected set; }
    public DateTime? LastUpdateAt { get; protected set; }
    public string LastUpdateBy { get; protected set; }

    protected AuditableResponse(Guid externalId, DateTime? createdAt, string createdBy, DateTime? lastUpdateAt, string lastUpdateBy) : base(externalId)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastUpdateAt = lastUpdateAt;
        LastUpdateBy = lastUpdateBy;
    }
}
