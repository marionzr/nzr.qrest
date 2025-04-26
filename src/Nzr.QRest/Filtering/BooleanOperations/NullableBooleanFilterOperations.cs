using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.BooleanOperations;

/// <summary>
/// Represents Nullable Boolean filter operations.
/// </summary>
public record NullableBooleanFilterOperations : BaseBooleanFilterOperations, IFilterOperations<bool?>
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
    /// <returns>An expression representing the combined Boolean filter operations.</returns>
    public Expression<Func<bool?, bool>>? ToExpression()
    {
        if (IsAllNull(EqualsTo, IsNull, IsNotNull))
        {
            return null;
        }

        Expression<Func<bool?, bool>> expression = value => true;
        expression = expression.ApplyNullCheck(IsNull, IsNotNull);

        // Only apply other checks if we're not explicitly looking for nulls
        if (!IsNull.HasValue || !IsNull.Value || !IsNotNull.HasValue || IsNotNull.Value)
        {
            if (EqualsTo.HasValue)
            {
                expression = expression.And(value => value == EqualsTo.Value);
            }

            if (NotEqualsTo.HasValue)
            {
                expression = expression.And(value => value != NotEqualsTo.Value);
            }
        }

        return expression;
    }
}
