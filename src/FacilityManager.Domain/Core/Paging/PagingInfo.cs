namespace FacilityManager.Domain.Core.Paging;

public class PagingInfo
{
    public PagingInfo(PagingQuery paging, int totalItems)
    {
        PageNumber = paging.PageNumber;
        PageSize = paging.PageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)paging.PageSize);
    }

    public PagingInfo(int totalItems)
    {
        PageNumber = 1;
        PageSize = totalItems;
        TotalItems = totalItems;
        TotalPages = 1;
    }

    public PagingInfo()
    {
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public int TotalItems { get; }

    public int TotalPages { get; }

    public bool HasNextPage => PageNumber < TotalPages;

    public bool HasPreviousPage => PageNumber > 1;
}