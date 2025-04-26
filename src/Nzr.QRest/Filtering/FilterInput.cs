using System.Linq.Expressions;
using System.Reflection;

namespace Nzr.QRest.Filtering;

/// <summary>
/// Abstract base class for filter operations.
/// </summary>
/// <typeparam name="TEntity">The type of entity to filter.</typeparam>
public abstract record FilterInput<TEntity>
{
    private Dictionary<PropertyInfo, PropertyInfo>? _filterProperties;

    /// <summary>
    /// Converts the filter input into an expression that can be used to filter a query.
    /// </summary>
    /// <returns>An expression representing the filter logic.</returns>
    public virtual Expression<Func<TEntity, bool>> ToExpression()
    {
        Expression<Func<TEntity, bool>> expression = x => true;

        // Configure filter properties if not already done
        Configure();

        // Apply all configured filters
        if (_filterProperties != null)
        {
            foreach (var (filterProperty, entityProperty) in _filterProperties)
            {
                var filterValue = filterProperty.GetValue(this);
                if (filterValue != null)
                {
                    // Get the generic type argument of IFilterOperations<T>
                    var filterOperationType = filterProperty.PropertyType
                        .GetInterfaces()
                        .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IFilterOperations<>));
                    var propertyType = filterOperationType.GetGenericArguments()[0];

                    // Create and invoke the generic ApplyFilter method
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                    var methodInfo = typeof(FilterInput<TEntity>)
                        .GetMethod(nameof(ApplyFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(typeof(TEntity), propertyType);
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields

                    expression = (Expression<Func<TEntity, bool>>)methodInfo.Invoke(
                        null,
                        [expression, filterValue, entityProperty.Name])!;
                }
            }
        }

        return expression;
    }

    /// <summary>
    /// Configures the filter properties by analyzing the filter input type and matching properties with the entity type.
    /// Override this method to customize the filter configuration.
    /// </summary>
    protected virtual void Configure()
    {
        if (_filterProperties != null)
        {
            return;
        }

        _filterProperties = [];

        var filterProperties = GetType().GetProperties()
            .Where(p => p.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IFilterOperations<>)))
            .ToList();

        var entityProperties = typeof(TEntity).GetProperties();

        foreach (var filterProperty in filterProperties)
        {
            // Match filter property name with entity property
            var entityProperty = entityProperties.FirstOrDefault(ep =>
                ep.Name.Equals(filterProperty.Name, StringComparison.OrdinalIgnoreCase));

            if (entityProperty != null)
            {
                // Verify property types are compatible
                var filterOperationType = filterProperty.PropertyType
                    .GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IFilterOperations<>));
                var filterPropertyType = filterOperationType.GetGenericArguments()[0];

                if (IsCompatibleType(entityProperty.PropertyType, filterPropertyType))
                {
                    _filterProperties.Add(filterProperty, entityProperty);
                }
            }
        }
    }

    private static bool IsCompatibleType(Type entityType, Type filterType)
    {
        // Handle nullable value types
        if (entityType.IsGenericType && entityType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            entityType = entityType.GetGenericArguments()[0];
        }

        return entityType == filterType;
    }


    /// <summary>
    /// Configures the filter properties by analyzing the filter input type and matching properties with the entity type.
    /// </summary>
    /// <typeparam name="T">The type of entity to filter.</typeparam>
    /// <typeparam name="TProp">The type of the property to filter.</typeparam>
    /// <param name="expression">The current expression to apply the filter to.</param>
    /// <param name="filter">The filter operations to apply.</param>
    /// <param name="propertyName">The name of the property to filter.</param>
    /// <returns>The updated expression with the filter applied.</returns>
    protected static Expression<Func<T, bool>> ApplyFilter<T, TProp>(
        Expression<Func<T, bool>> expression,
        IFilterOperations<TProp>? filter,
        string propertyName)
    {
        if (filter?.ToExpression() is Expression<Func<TProp, bool>> filterExpression)
        {
            var param = Expression.Parameter(typeof(T), $"x{propertyName}");
            var property = Expression.Property(param, propertyName);
            var body = Expression.Invoke(filterExpression, property);
            // <code>var lambda = Expression.Lambda<Func<T, bool>>(body, $"x{propertyName}"param);</code>
            var lambda = Expression.Lambda<Func<T, bool>>(body, param);

            return expression.And(lambda);
        }

        return expression;
    }
}
