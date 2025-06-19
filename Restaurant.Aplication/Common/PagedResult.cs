namespace Restaurants.Application.Common;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;
        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
        // TotalPages = 12/5 = 2.4 => 3 (ceil)(redondear hacia arriba)
        // PageSize = 5, PageNumber = 2
        // Skip = PageSize * (PageNumber - 1) => 5 * (2 - 1) = 5
        // ItemsFrom = 5 + 1 = 6
        // ItemsTo = 6 + 5 - 1 = 10
    }
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
