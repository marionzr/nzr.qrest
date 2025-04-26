using Nzr.QRest.Filtering;
using Nzr.QRest.Paging;
using Nzr.QRest.Sorting;

namespace Nzr.QRest;

/// <summary>
/// Represents the full query input combining filter, sort, and pagination.
/// </summary>
/// <typeparam name="TEntity">The type of entity to query.</typeparam>
/// <typeparam name="TFilter">The type of filter input.</typeparam>
/// <typeparam name="TSort">The type of sort input.</typeparam>
public record QueryInput<TEntity, TFilter, TSort>
    where TFilter : FilterInput<TEntity>
    where TSort : SortInput<TEntity>
{
    /// <summary>
    /// The filter input for the query.
    /// </summary>
    public TFilter? Filter { get; init; }

    /// <summary>
    /// The sort input for the query.
    /// </summary>
    public TSort? Sort { get; init; }

    /// <summary>
    /// The pagination input for the query.
    /// </summary>
    public PaginationInput? Pagination { get; init; }
}
