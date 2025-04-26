using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.EnumOperations;

/// <summary>
/// Represents Enum filter operations.
/// </summary>
/// <typeparam name="TEnum">The Enum type.</typeparam>
public record EnumFilterOperations<TEnum> : BaseEnumFilterOperations<TEnum>, IFilterOperations<TEnum> where TEnum : struct, Enum
{
    /// <summary>
    /// Converts the filter operations to an expression.
    /// </summary>
    /// <returns>An expression representing the combined Enum filter operations.</returns>
    public Expression<Func<TEnum, bool>>? ToExpression()
    {

        if (IsAllNull(EqualsTo, NotEqualsTo, In, NotIn))
        {
            return null;
        }

        Expression<Func<TEnum, bool>> expression = value => true;

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
            expression = expression.And(value => In.Contains(value));
        }

        if (NotIn?.Length > 0)
        {
            expression = expression.And(value => !NotIn.Contains(value));
        }

        return expression;
    }
}
