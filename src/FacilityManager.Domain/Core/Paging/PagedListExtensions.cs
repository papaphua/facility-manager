namespace FacilityManager.Domain.Core.Paging;

public static class PagedListExtensions
{
    public static PagedList<T> AsPagedList<T>(this IEnumerable<T> collection, PagingInfo info)
    {
        return new PagedList<T>(collection, info);
    }
}