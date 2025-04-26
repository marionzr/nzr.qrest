using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.BooleanOperations;

/// <summary>
/// Represents Boolean filter operations.
/// </summary>
public record BooleanFilterOperations : BaseBooleanFilterOperations, IFilterOperations<bool>
{
    /// <summary>
    /// Converts the filter operations to an expression.
    /// </summary>
    /// <returns>An expression representing the combined Boolean filter operations.</returns>
    public Expression<Func<bool, bool>>? ToExpression()
    {
        if (IsAllNull(EqualsTo, NotEqualsTo))
        {
            return null;
        }

        Expression<Func<bool, bool>> expression = value => true;

        if (EqualsTo.HasValue)
        {
            expression = expression.And(value => value == EqualsTo.Value);
        }

        if (NotEqualsTo.HasValue)
        {
            expression = expression.And(value => value != NotEqualsTo.Value);
        }

        return expression;
    }
}
