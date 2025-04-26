using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Nzr.QRest.Sorting;

/// <summary>
/// Abstract base class for sorting operations.
/// </summary>
/// <typeparam name="TEntity">The type of entity to sort.</typeparam>
public record class SortInput<TEntity>
{
    private Dictionary<PropertyInfo, PropertyInfo>? _sortProperties;

    /// <summary>
    /// Configures the sort properties by analyzing the sort input type and matching properties with the entity type.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public virtual IOrderedQueryable<TEntity> ApplySort(IQueryable<TEntity> query)
    {
        // Configure sort properties if not already done
        Configure();

        if (_sortProperties == null || !_sortProperties.Any())
        {
            // Default sorting by first property (assuming Id exists)
            return query.OrderBy(x => EF.Property<object>(x!, "Id"));
        }

        IOrderedQueryable<TEntity>? orderedQuery = null;

        foreach (var (sortProperty, entityProperty) in _sortProperties)
        {
            var direction = (SortDirection?)sortProperty.GetValue(this);
            if (direction.HasValue)
            {
                // Create parameter expression for the lambda
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var property = Expression.Property(parameter, entityProperty.Name);
                var lambda = Expression.Lambda(property, parameter);

                // Create and compile the expression
                var methodName = GetSortMethodName(orderedQuery, direction);

                var method = typeof(Queryable)
                    .GetMethods()
                    .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TEntity), entityProperty.PropertyType);

                var source = orderedQuery ?? query;
                orderedQuery = (IOrderedQueryable<TEntity>)method.Invoke(null, new object[] { source, lambda })!;
            }
        }

        return orderedQuery ?? query.OrderBy(x => EF.Property<object>(x!, "Id"));

        static string GetSortMethodName(IOrderedQueryable<TEntity>? orderedQuery, SortDirection? direction)
        {
            var orderByMethodName = direction == SortDirection.ASC ? "OrderBy" : "OrderByDescending";
            var thenByMethodName = direction == SortDirection.ASC ? "ThenBy" : "ThenByDescending";

            return orderedQuery == null ? orderByMethodName : thenByMethodName;
        }
    }

    /// <summary>
    /// Configures the sort properties by analyzing the sort input type and matching properties with the entity type.
    /// </summary>
    protected virtual void Configure()
    {
        if (_sortProperties != null)
        {
            return;
        }

        _sortProperties = [];

        var sortProperties = GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(SortDirection?));

        var entityProperties = typeof(TEntity).GetProperties();

        foreach (var sortProperty in sortProperties)
        {
            // Match sort property name with entity property
            var entityProperty = entityProperties.FirstOrDefault(ep =>
                ep.Name.Equals(sortProperty.Name, StringComparison.OrdinalIgnoreCase));

            if (entityProperty != null)
            {
                _sortProperties.Add(sortProperty, entityProperty);
            }
        }
    }
}
