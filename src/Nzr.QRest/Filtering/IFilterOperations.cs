using System.Linq.Expressions;

namespace Nzr.QRest.Filtering;

/// <summary>
/// Defines a base interface for all filter operations.
/// </summary>
/// <typeparam name="T">The type of the value being filtered.</typeparam>
public interface IFilterOperations<T>
{
    /// <summary>
    /// Converts the filter operation to an expression.
    /// </summary>
    /// <returns>An <see cref="Expression{TDelegate}"/> representing the filter operation.</returns>
    Expression<Func<T, bool>>? ToExpression();
}
