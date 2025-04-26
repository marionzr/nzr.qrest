namespace Nzr.QRest.Filtering.DateTimeOperations;

/// <summary>
/// Represents DateTime filter operations.
/// </summary>
public abstract record BaseDateTimeFilterOperations<TDateTime> where TDateTime : struct
{
    /// <summary>
    /// The value for an equality filter.
    /// </summary>
    public TDateTime? EqualsTo { get; init; }

    /// <summary>
    /// The value for a not-equal filter.
    /// </summary>
    public TDateTime? NotEqualsTo { get; init; }

    /// <summary>
    /// The value for a before filter.
    /// </summary>
    public TDateTime? Before { get; init; }

    /// <summary>
    /// The value for a before-or-equal filter.
    /// </summary>
    public TDateTime? BeforeOrEqual { get; init; }

    /// <summary>
    /// The value for an after filter.
    /// </summary>
    public TDateTime? After { get; init; }

    /// <summary>
    /// The value for an after-or-equal filter.
    /// </summary>
    public TDateTime? AfterOrEqual { get; init; }

    /// <summary>
    /// The lower bound for a date range filter.
    /// </summary>
    public TDateTime? From { get; init; }

    /// <summary>
    /// The upper bound for a date range filter.
    /// </summary>
    public TDateTime? To { get; init; }

    /// <summary>
    /// A value to filter by date-only equality.
    /// </summary>
    public DateOnly? DateEquals { get; init; }

    /// <summary>
    /// A value to filter by time-only equality.
    /// </summary>
    public TimeOnly? TimeEquals { get; init; }

    /// <summary>
    /// The year to match.
    /// </summary>
    public int? YearEquals { get; init; }

    /// <summary>
    /// The month to match.
    /// </summary>
    public int? MonthEquals { get; init; }

    /// <summary>
    /// The day to match.
    /// </summary>
    public int? DayEquals { get; init; }
}
