namespace Application.SeedWork;

public class PaginatedResponse<T> where T : BaseRequestResponse
{
    public IEnumerable<T> Result { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }

    public PaginatedResponse(IEnumerable<T> result, int page, int perPage, int total)
    {
        Result = result;
        Page = page;
        PerPage = perPage;
        Total = total;
    }
}
