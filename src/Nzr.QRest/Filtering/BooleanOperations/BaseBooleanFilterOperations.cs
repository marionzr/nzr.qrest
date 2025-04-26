namespace Nzr.QRest.Filtering.BooleanOperations;

/// <summary>
/// Represents Boolean filter operations.
/// </summary>
public abstract record BaseBooleanFilterOperations
{
    /// <summary>
    /// The value for an equality filter.
    /// </summary>
    public bool? EqualsTo { get; init; }

    /// <summary>
    /// The value for an equality filter.
    /// </summary>
    public bool? NotEqualsTo { get; init; }
}
