using System.Linq.Expressions;

namespace Nzr.QRest.Extensions;

/// <summary>
/// Helper extension methods for working with expressions.
/// </summary>
public static class ExpressionExtensions
{
    /// <summary>
    /// Combines two expressions with an AND condition.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="expr1">The first expression.</param>
    /// <param name="expr2">The second expression.</param>
    /// <returns>The combined expression.</returns>
    public static Expression<Func<T, bool>> Andx<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(expr1.Body, invokedExpr),
            expr1.Parameters);
    }

    /// <summary>
    /// Combines two expressions with an AND condition.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="expr1">The first expression.</param>
    /// <param name="expr2">The second expression.</param>
    /// <returns>The combined expression.</returns>
    public static Expression<Func<T?, bool>> And<T>(
        this Expression<Func<T?, bool>> expr1,
        Expression<Func<T?, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);

        return Expression.Lambda<Func<T?, bool>>(
            Expression.AndAlso(expr1.Body, invokedExpr),
            expr1.Parameters);
    }

    /// <summary>
    /// Combines two expressions with an AND condition.
    /// </summary>
    /// <param name="expr1">The first expression.</param>
    /// <param name="expr2">The second expression.</param>
    /// <returns>The combined expression.</returns>
    public static Expression<Func<string, bool>> And(
        this Expression<Func<string, bool>> expr1,
        Expression<Func<string, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);

        return Expression.Lambda<Func<string, bool>>(
            Expression.AndAlso(expr1.Body, invokedExpr),
            expr1.Parameters);
    }
}
