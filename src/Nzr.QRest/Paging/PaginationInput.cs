using System.ComponentModel.DataAnnotations;

namespace Nzr.QRest.Paging;

/// <summary>
/// Represents the pagination input for a query.
/// </summary>
public record PaginationInput
{
    /// <summary>
    /// The page number to fetch (default is 1).
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    /// <summary>
    /// The number of items per page (default is 10).
    /// </summary>
    [Range(1, 100)]
    public int PageSize { get; init; } = 10;
}
