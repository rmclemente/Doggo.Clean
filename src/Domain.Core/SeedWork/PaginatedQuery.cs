namespace Domain.Core.SeedWork;

public abstract class PaginatedQuery : Query
{
    public int Page { get; private set; }
    public int PerPage { get; private set; }
    public int Take => PerPage;
    public int Skip => Page * PerPage - PerPage;

    public PaginatedQuery(int page, int rows)
    {
        Page = page <= 0 ? 1 : page;
        PerPage = rows <= 0 ? 10 : rows;
    }
}
