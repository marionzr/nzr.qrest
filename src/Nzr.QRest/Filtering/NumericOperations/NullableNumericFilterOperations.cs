using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.NumericOperations;

/// <summary>
/// Represents numeric filter operations for all numeric types.
/// </summary>
/// <typeparam name="T">The numeric type.</typeparam>
public record NullableNumericFilterOperations<T> : BaseNumericFilterOperations<T>, IFilterOperations<T?> where T : struct, IComparable<T>
{
    /// <summary>
    /// A flag for checking if the value is null.
    /// </summary>
    public bool? IsNull { get; init; }

    /// <summary>
    /// A flag for checking if the value is not null.
    /// </summary>
    public bool? IsNotNull { get; init; }

    /// <summary>
    /// Converts the filter operations to an expression.
    /// </summary>
    /// <returns>An expression representing the combined numeric filter operations.</returns>
    public Expression<Func<T?, bool>>? ToExpression()
    {
        if (IsAllNull(
            EqualsTo, NotEqualsTo,
            GreaterThan, GreaterThanOrEqual,
            LessThan, LessThanOrEqual,
            In, NotIn,
            From, To,
            IsNull, IsNotNull))
        {
            return null;
        }

        Expression<Func<T?, bool>> expression = value => true;

        expression = expression.ApplyNullCheck(IsNull, IsNotNull);

        // Only apply other checks if we're not explicitly looking for nulls
        if (!IsNull.HasValue || !IsNull.Value || !IsNotNull.HasValue || IsNotNull.Value)
        {
            if (EqualsTo.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.CompareTo(EqualsTo.Value) == 0);
            }

            if (NotEqualsTo.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.CompareTo(NotEqualsTo.Value) != 0);
            }

            if (GreaterThan.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.CompareTo(GreaterThan.Value) > 0);
            }

            if (GreaterThanOrEqual.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.CompareTo(GreaterThanOrEqual.Value) >= 0);
            }

            if (LessThan.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.CompareTo(LessThan.Value) < 0);
            }

            if (LessThanOrEqual.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.CompareTo(LessThanOrEqual.Value) <= 0);
            }

            if (In?.Length > 0)
            {
                expression = expression.And(value => value != null && In.Contains(value.Value));
            }

            if (NotIn?.Length > 0)
            {
                expression = expression.And(value => value != null && !NotIn.Contains(value.Value));
            }

            if (From.HasValue && To.HasValue)
            {
                expression = expression.And(value =>
                    value != null && value.Value.CompareTo(From.Value) >= 0 && value != null && value.Value.CompareTo(To.Value) <= 0);
            }
        }

        return expression;
    }
}
