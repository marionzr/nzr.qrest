namespace Nzr.QRest.Filtering.StringOperations;

/// <summary>
/// Represents string filter operations for string types.
/// </summary>
public abstract record BaseStringFilterOperations
{
    /// <summary>
    /// The value for an equality filter.
    /// </summary>
    public string? EqualsTo { get; init; }

    /// <summary>
    /// The value for a for inequality filter.
    /// </summary>
    public string? NotEqualsTo { get; init; }

    /// <summary>
    /// The value for a check for containment.
    /// </summary>
    public string? Contains { get; init; }

    /// <summary>
    /// The value for a check for non-containment.
    /// </summary>
    public string? DoesNotContain { get; init; }

    /// <summary>
    /// The value for a check for the string starting with the given value.
    /// </summary>
    public string? StartsWith { get; init; }

    /// <summary>
    /// The value for a check for the string ending with the given value.
    /// </summary>
    public string? EndsWith { get; init; }

    /// <summary>
    /// A list of values for an 'in' filter.
    /// </summary>
    public string[]? In { get; init; }

    /// <summary>
    /// A list of values for a 'not-in' filter.
    /// </summary>
    public string[]? NotIn { get; init; }
}
