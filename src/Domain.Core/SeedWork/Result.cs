using System.Net;

namespace Domain.Core.SeedWork;

public abstract class Result
{
    public object Content { get; protected set; }
    public HttpStatusCode StatusCode { get; protected set; }
    public string ResultType => GetType().Name;

    protected Result(HttpStatusCode statusCode) => StatusCode = statusCode;
}
