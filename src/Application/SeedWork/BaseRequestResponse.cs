namespace Application.SeedWork;

public abstract class BaseRequestResponse
{
    public Guid ExternalId { get; set; }

    protected BaseRequestResponse(Guid externalId)
    {
        ExternalId = externalId;
    }
}
