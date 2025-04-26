using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.EnumOperations;

/// <summary>
/// Represents Enum filter operations.
/// </summary>
/// <typeparam name="TEnum">The Enum type.</typeparam>
public record NullableEnumFilterOperations<TEnum> : BaseEnumFilterOperations<TEnum>, IFilterOperations<TEnum?> where TEnum : struct, Enum
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
    /// <returns>An expression representing the combined Enum filter operations.</returns>
    public Expression<Func<TEnum?, bool>>? ToExpression()
    {

        if (IsAllNull(EqualsTo, NotEqualsTo, In, NotIn, IsNull, IsNotNull))
        {
            return null;
        }

        Expression<Func<TEnum?, bool>> expression = value => true;

        expression = expression.ApplyNullCheck(IsNull, IsNotNull);

        // Only apply other checks if we're not explicitly looking for nulls
        if (!IsNull.HasValue || !IsNull.Value || !IsNotNull.HasValue || IsNotNull.Value)
        {
            if (EqualsTo.HasValue)
            {
                expression = expression.And(value => value.Equals(EqualsTo.Value));
            }

            if (NotEqualsTo.HasValue)
            {
                expression = expression.And(value => !value.Equals(NotEqualsTo.Value));
            }

            if (In?.Length > 0)
            {
                expression = expression.And(value => value != null && In.Contains(value.Value));
            }

            if (NotIn?.Length > 0)
            {
                expression = expression.And(value => value != null && !NotIn.Contains(value.Value));
            }
        }


        return expression;
    }
}
