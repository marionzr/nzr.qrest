using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.DateTimeOperations;

/// <summary>
/// Represents DateTime filter operations.
/// </summary>
public record DateTimeOffsetFilterOperations : BaseDateTimeFilterOperations<DateTimeOffset>, IFilterOperations<DateTimeOffset>
{
    /// <summary>
    /// Converts the filter operations to an expression.
    /// </summary>
    /// <returns>An expression representing the combined DateTime filter operations.</returns>
    public Expression<Func<DateTimeOffset, bool>>? ToExpression()
    {
        if (IsAllNull(
            EqualsTo, NotEqualsTo,
            Before, BeforeOrEqual,
            After, AfterOrEqual,
            From, To,
            DateEquals, TimeEquals,
            YearEquals, MonthEquals, DayEquals))
        {
            return null;
        }

        Expression<Func<DateTimeOffset, bool>> expression = value => true;

        if (EqualsTo.HasValue)
        {
            expression = expression.And(value => value == EqualsTo.Value);
        }

        if (NotEqualsTo.HasValue)
        {
            expression = expression.And(value => value != NotEqualsTo.Value);
        }

        if (Before.HasValue)
        {
            expression = expression.And(value => value < Before.Value);
        }

        if (BeforeOrEqual.HasValue)
        {
            expression = expression.And(value => value <= BeforeOrEqual.Value);
        }

        if (After.HasValue)
        {
            expression = expression.And(value => value > After.Value);
        }

        if (AfterOrEqual.HasValue)
        {
            expression = expression.And(value => value >= AfterOrEqual.Value);
        }

        if (From.HasValue && To.HasValue)
        {
            expression = expression.And(value => value >= From.Value && value <= To.Value);
        }

        if (DateEquals.HasValue)
        {
            expression = expression.And(value => DateOnly.FromDateTime(value.DateTime) == DateEquals.Value);
        }

        if (TimeEquals.HasValue)
        {
            expression = expression.And(value => TimeOnly.FromDateTime(value.DateTime) == TimeEquals.Value);
        }

        if (YearEquals.HasValue)
        {
            expression = expression.And(value => value.Year == YearEquals.Value);
        }

        if (MonthEquals.HasValue)
        {
            expression = expression.And(value => value.Month == MonthEquals.Value);
        }

        if (DayEquals.HasValue)
        {
            expression = expression.And(value => value.Day == DayEquals.Value);
        }

        return expression;
    }
}
