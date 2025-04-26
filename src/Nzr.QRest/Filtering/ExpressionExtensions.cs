using System.Linq.Expressions;

namespace Nzr.QRest.Filtering;

/// <summary>
/// Represents the extension methods for expressions.
/// </summary>
public static class ExpressionExtensions
{
    /// <summary>
    /// Combines the null checks with the given expression.
    /// </summary>
    /// <param name="expression">The expression to combine with the null checks.</param>
    /// <param name="isNull">The flag for checking if the value is null.</param>
    /// <param name="isNotNull">The flag for checking if the value is not null.</param>
    /// <returns>A new expression representing the combined null checks.</returns>
    public static Expression<Func<string?, bool>> ApplyNullCheck(this Expression<Func<string?, bool>> expression, bool? isNull, bool? isNotNull)
    {
        if (isNull.HasValue)
        {
            expression = expression.And(value => isNull.Value ? value == null : value != null);
        }

        if (isNotNull.HasValue)
        {
            expression = expression.And(value => isNotNull.Value ? value != null : value == null);
        }

        return expression;
    }

    /// <summary>
    /// Combines the null checks with the given expression.
    /// </summary>
    /// <param name="expression">The expression to combine with the null checks.</param>
    /// <param name="isNull">The flag for checking if the value is null.</param>
    /// <param name="isNotNull">The flag for checking if the value is not null.</param>
    /// <returns>A new expression representing the combined null checks.</returns>
    public static Expression<Func<T?, bool>> ApplyNullCheck<T>(this Expression<Func<T?, bool>> expression, bool? isNull, bool? isNotNull) where T : struct
    {
        if (isNull.HasValue)
        {
            expression = expression.And(value => isNull.Value ? value == null : value != null);
        }

        if (isNotNull.HasValue)
        {
            expression = expression.And(value => isNotNull.Value ? value != null : value == null);
        }

        return expression;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var combined = Expression.AndAlso(
            Expression.Invoke(first, parameter),
            Expression.Invoke(second, parameter));

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
}
