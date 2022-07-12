using System.Net;

namespace Domain.Core.SeedWork;

public class CommandResult : Result
{
    public Guid? ResourceId { get; private set; }
    public string Message { get; private set; }

    public CommandResult(HttpStatusCode statusCode) : base(statusCode) { }

    /// <summary>
    /// Create a CommandResult with status code 200 (OK).
    /// </summary>
    /// <returns>CommandResult</returns>
    public static CommandResult SuccessResult() => new(HttpStatusCode.OK);

    // <summary>
    /// Create a CommandResult with status code 201 (Created).
    /// </summary>
    /// <returns>CommandResult</returns>
    public static CommandResult CreatedResult() => new(HttpStatusCode.Created);

    // <summary>
    /// Create a CommandResult with status code 204 (NoContent).
    /// </summary>
    /// <returns>CommandResult</returns>
    public static CommandResult NoContentResult() => new(HttpStatusCode.NoContent);

    // <summary>
    /// Create a CommandResult with status code 404 (NotFound).
    /// </summary>
    /// <returns>CommandResult</returns>
    public static CommandResult NotFoundResult() => new(HttpStatusCode.NotFound);

    /// <summary>
    /// Create a CommandResult with status code 400 (BadRequest).
    /// </summary>
    /// <returns>CommandResult</returns>
    public static CommandResult ValidationErrorResult() => new(HttpStatusCode.BadRequest);

    /// <summary>
    /// Add Message to an existing CommandResult instance.
    /// </summary>
    /// <param name="message"></param>
    /// <returns>CommandResult</returns>
    public CommandResult WithMessage(string message)
    {
        Message = message;
        return this;
    }

    /// <summary>
    /// Add Content to an existing CommandResult instance.
    /// </summary>
    /// <param name="content"></param>
    /// <returns>CommandResult</returns>
    public CommandResult WithContent(object content)
    {
        Content = content;
        return this;
    }

    /// <summary>
    /// Add ResourceId to an existing CommandResult instance.
    /// </summary>
    /// <param name="resourdeId"></param>
    /// <returns>CommandResult</returns>
    public CommandResult WithResourceId(Guid resourdeId)
    {
        ResourceId = resourdeId;
        return this;
    }
}
