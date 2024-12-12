namespace FacilityManager.Domain.Core.Paging;

public sealed class PagedResponse<TData>
{
    public PagedResponse(PagedList<TData> data, PagingInfo info)
    {
        Data = data;
        Info = info;
    }

    public PagedList<TData> Data { get; set; }

    public PagingInfo Info { get; set; }
}