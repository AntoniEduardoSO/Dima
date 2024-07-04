using System.Text.Json.Serialization;

namespace Dima.core.Responses;

public class PagedResponse<TData> : Response<TData>
{
    [JsonConstructor]
    public PagedResponse(
        TData? data,
        int totalcount,
        int currentpage = 1,
        int pagesize = Configuration.DefaultPageSize)
        : base(data)
    {
        Data = data;
        TotalCount = totalcount;
        CurrentPage = currentpage;
        PageSize = pagesize;
    }

    public PagedResponse(
        TData? data,
        int code = Configuration.DefaultStatusCode,
        string? message = null)
        : base(data, code, message)
    {
    }

    public int CurrentPage {get;set;}
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int TotalCount {get;set;}
}