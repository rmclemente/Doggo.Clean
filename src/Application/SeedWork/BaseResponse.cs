namespace Application.SeedWork;

public abstract class BaseResponse
{
    public Guid ExternalId { get; set; }

    protected BaseResponse(Guid externalId)
    {
        ExternalId = externalId;
    }
}
