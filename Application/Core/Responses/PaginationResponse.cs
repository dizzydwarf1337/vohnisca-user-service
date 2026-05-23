namespace Application.Core.Responses;

public class PaginationResponse<TRow>
{
    public int Page { get; init; }

    public int PageSize { get; init; }

    public bool HasNextPage { get; init; }

    public int PagesCount { get; init; }

    public TRow[] Items { get; init; }

    public PaginationResponse(int page, int pageSize, bool hasNextPage, int pagesCount, TRow[] items)
    {
        Page = page;
        PageSize = pageSize;
        HasNextPage = hasNextPage;
        PagesCount = pagesCount;
        Items = items;
    }
}