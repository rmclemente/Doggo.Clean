using System.Net;

namespace Domain.Core.SeedWork;

public class QueryResult : Result
{
    private QueryResult(HttpStatusCode statusCode) : base(statusCode) { }

    /// <summary>
    /// Create a QueryResult with status code 200 (OK).
    /// </summary>
    /// <returns>QueryResult</returns>
    public static QueryResult SuccessResult() => new(HttpStatusCode.OK);

    /// <summary>
    /// Create a QueryResult with status code 404 (NotFound).
    /// </summary>
    /// <returns>QueryResult</returns>
    public static QueryResult NotFoundResult() => new(HttpStatusCode.NotFound);

    /// <summary>
    /// Create a QueryResult with status code 404 (NotFound) if Content is Null
    /// or a 200 (OK) if not.
    /// </summary>
    /// <returns>QueryResult</returns>
    public static QueryResult ResultValidatedResult(object content) =>
        content is null ?
        NotFoundResult() :
        SuccessResult().WithContent(content);

    /// <summary>
    /// Add Content to an existing Success Result instance.
    /// </summary>
    /// <param name="content"></param>
    /// <returns>QueryResult</returns>
    public QueryResult WithContent(object content)
    {
        Content = content;
        return this;
    }
}
