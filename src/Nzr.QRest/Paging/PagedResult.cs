namespace Nzr.QRest.Paging;

/// <summary>
/// Represents a paginated result of a query.
/// </summary>
/// <typeparam name="T">The type of the items in the result.</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// The list of items in the current page.
    /// </summary>
    public IEnumerable<T> Items { get; set; } = [];

    /// <summary>
    /// The total number of items available across all pages.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// The current page number.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The total number of pages.
    /// </summary>
    public int TotalPages { get; set; }
}
