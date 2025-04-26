namespace Nzr.QRest.Filtering.NumericOperations;

/// <summary>
/// Represents numeric filter operations for all numeric types.
/// </summary>
/// <typeparam name="T">The numeric type.</typeparam>
public abstract record BaseNumericFilterOperations<T> where T : struct
{
    /// <summary>
    /// The value for an equality filter.
    /// </summary>
    public T? EqualsTo { get; init; }

    /// <summary>
    /// The value for a not-equal filter.
    /// </summary>
    public T? NotEqualsTo { get; init; }

    /// <summary>
    /// The value for a greater-than filter.
    /// </summary>
    public T? GreaterThan { get; init; }

    /// <summary>
    /// The value for a greater-than-or-equal filter.
    /// </summary>
    public T? GreaterThanOrEqual { get; init; }

    /// <summary>
    /// The value for a less-than filter.
    /// </summary>
    public T? LessThan { get; init; }

    /// <summary>
    /// The value for a less-than-or-equal filter.
    /// </summary>
    public T? LessThanOrEqual { get; init; }

    /// <summary>
    /// A list of values for an 'in' filter.
    /// </summary>
    public T[]? In { get; init; }

    /// <summary>
    /// A list of values for a 'not-in' filter.
    /// </summary>
    public T[]? NotIn { get; init; }

    /// <summary>
    /// The lower bound for a range filter.
    /// </summary>
    public T? From { get; init; }

    /// <summary>
    /// The upper bound for a range filter.
    /// </summary>
    public T? To { get; init; }
}
