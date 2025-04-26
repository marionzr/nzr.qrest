using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.StringOperations;

/// <summary>
/// Represents string-based filter operations.
///
/// We don't need a separate NullableStringFilterOperations because string is already a reference type in C#,
/// meaning it's inherently nullable.
/// </summary>
public record StringFilterOperations : BaseStringFilterOperations, IFilterOperations<string>
{
    /// <summary>
    /// Converts the filter operations to an expression.
    /// </summary>
    /// <returns>An expression representing the filter logic, or null if no filter is set.</returns>
    public Expression<Func<string, bool>>? ToExpression()
    {
        if (IsAllNull(
            EqualsTo, NotEqualsTo,
            Contains, DoesNotContain,
            StartsWith, EndsWith,
            In, NotIn))
        {
            return null;
        }

        Expression<Func<string, bool>> expression = value => true;

        // Only apply other checks if we're not explicitly looking for nulls
        if (EqualsTo != null)
        {
            expression = expression.And(value => value == EqualsTo);
        }

        if (NotEqualsTo != null)
        {
            expression = expression.And(value => value != NotEqualsTo);
        }

        if (Contains != null)
        {
            expression = expression.And(value => value != null && value.Contains(Contains));
        }

        if (DoesNotContain != null)
        {
            expression = expression.And(value => value != null && !value.Contains(DoesNotContain));
        }

        if (StartsWith != null)
        {
            expression = expression.And(value => value != null && value.StartsWith(StartsWith));
        }

        if (EndsWith != null)
        {
            expression = expression.And(value => value != null && value.EndsWith(EndsWith));
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
