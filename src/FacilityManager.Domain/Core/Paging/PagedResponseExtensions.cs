namespace FacilityManager.Domain.Core.Paging;

public static class PagedResponseExtensions
{
    public static PagedResponse<TData> ToPagedResponse<TData>(this PagedList<TData> list)
    {
        return new PagedResponse<TData>(list, list.Info);
    }
}