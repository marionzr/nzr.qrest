using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.NumericOperations;

/// <summary>
/// Represents numeric filter operations for all numeric types.
/// </summary>
/// <typeparam name="T">The numeric type.</typeparam>
public record NumericFilterOperations<T> : BaseNumericFilterOperations<T>, IFilterOperations<T> where T : struct, IComparable<T>
{
    /// <summary>
    /// Converts the filter operations to an expression.
    /// </summary>
    /// <returns>An expression representing the combined numeric filter operations.</returns>
    public Expression<Func<T, bool>>? ToExpression()
    {
        if (IsAllNull(
            EqualsTo, NotEqualsTo,
            GreaterThan, GreaterThanOrEqual,
            LessThan, LessThanOrEqual,
            In, NotIn,
            From, To))
        {
            return null;
        }

        Expression<Func<T, bool>> expression = value => true;

        if (EqualsTo.HasValue)
        {
            expression = expression.And(value => value.CompareTo(EqualsTo.Value) == 0);
        }

        if (NotEqualsTo.HasValue)
        {
            expression = expression.And(value => value.CompareTo(NotEqualsTo.Value) != 0);
        }

        if (GreaterThan.HasValue)
        {
            expression = expression.And(value => value.CompareTo(GreaterThan.Value) > 0);
        }

        if (GreaterThanOrEqual.HasValue)
        {
            expression = expression.And(value => value.CompareTo(GreaterThanOrEqual.Value) >= 0);
        }

        if (LessThan.HasValue)
        {
            expression = expression.And(value => value.CompareTo(LessThan.Value) < 0);
        }

        if (LessThanOrEqual.HasValue)
        {
            expression = expression.And(value => value.CompareTo(LessThanOrEqual.Value) <= 0);
        }

        if (In?.Length > 0)
        {
            expression = expression.And(value => In.Contains(value));
        }

        if (NotIn?.Length > 0)
        {
            expression = expression.And(value => !NotIn.Contains(value));
        }

        if (From.HasValue && To.HasValue)
        {
            expression = expression.And(value =>
                value.CompareTo(From.Value) >= 0 && value.CompareTo(To.Value) <= 0);
        }

        return expression;

    }
}
