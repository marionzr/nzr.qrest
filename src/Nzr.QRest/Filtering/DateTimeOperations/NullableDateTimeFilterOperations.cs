using System.Linq.Expressions;
using static Nzr.ToolBox.Core.ToolBox;

namespace Nzr.QRest.Filtering.DateTimeOperations;

/// <summary>
/// Represents DateTime filter operations.
/// </summary>
public record NullableDateTimeFilterOperations : BaseDateTimeFilterOperations<DateTime>, IFilterOperations<DateTime?>
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
    /// <returns>An expression representing the combined DateTime filter operations.</returns>
    public Expression<Func<DateTime?, bool>>? ToExpression()
    {
        if (IsAllNull(
            EqualsTo, NotEqualsTo,
            Before, BeforeOrEqual,
            After, AfterOrEqual,
            From, To,
            IsNull, IsNotNull,
            DateEquals, TimeEquals,
            YearEquals, MonthEquals, DayEquals))
        {
            return null;
        }

        Expression<Func<DateTime?, bool>> expression = value => true;

        expression = ApplyDateTimeNullCheck(expression);

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
                expression = expression.And(value => value != null && DateOnly.FromDateTime(value.Value) == DateEquals.Value);
            }

            if (TimeEquals.HasValue)
            {
                expression = expression.And(value => value != null && TimeOnly.FromDateTime(value.Value) == TimeEquals.Value);
            }

            if (YearEquals.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.Year == YearEquals.Value);
            }

            if (MonthEquals.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.Month == MonthEquals.Value);
            }

            if (DayEquals.HasValue)
            {
                expression = expression.And(value => value != null && value.Value.Day == DayEquals.Value);
            }
        }

        return expression;
    }

    private Expression<Func<DateTime?, bool>> ApplyDateTimeNullCheck(Expression<Func<DateTime?, bool>> expression)
    {
        if (IsNull == true)
        {
            expression = expression.And(value => value == null);
        }

        if (IsNotNull == true)
        {
            expression = expression.And(value => value != null);
        }

        return expression;
    }
}
