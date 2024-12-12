namespace FacilityManager.Domain.Core.Paging;

public sealed class PagedList<T> : List<T>
{
    public PagedList(List<T> items, PagingQuery query, int totalItems)
    {
        Info = new PagingInfo(query, totalItems);
        AddRange(items);
    }

    public PagedList(IEnumerable<T> items, PagingInfo info)
    {
        Info = info;
        AddRange(items);
    }

    public PagedList(IList<T> items)
    {
        Info = new PagingInfo(items.Count);
        AddRange(items);
    }

    public PagedList()
    {
        Info = new PagingInfo();
    }

    public PagingInfo Info { get; }
}