using Microsoft.EntityFrameworkCore;

namespace Nzr.QRest.Paging;

/// <summary>
/// Helper extension methods for querying paginated results.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Converts the queryable into a paged result.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="query">The query to paginate.</param>
    /// <param name="pagination">The pagination settings.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paged result with items and pagination information.</returns>
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        PaginationInput pagination,
        CancellationToken cancellationToken = default)
    {
        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>
        {
            Items = items,
            TotalItems = totalItems,
            CurrentPage = pagination.Page,
            PageSize = pagination.PageSize,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pagination.PageSize)
        };
    }

    /// <summary>
    /// Converts the queryable into a paged result.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="query">The query to paginate.</param>
    /// <param name="pagination">The pagination settings.</param>
    /// <returns>A paged result with items and pagination information.</returns>
    public static PagedResult<T> ToPagedResult<T>(
        this IQueryable<T> query,
        PaginationInput pagination)
    {
        var totalItems = query.Count();

        var items = query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToList();

        return new PagedResult<T>
        {
            Items = items,
            TotalItems = totalItems,
            CurrentPage = pagination.Page,
            PageSize = pagination.PageSize,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pagination.PageSize)
        };
    }
}
