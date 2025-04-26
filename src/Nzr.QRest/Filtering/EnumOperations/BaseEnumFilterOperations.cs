namespace Nzr.QRest.Filtering.EnumOperations;

/// <summary>
/// Represents Enum filter operations.
/// </summary>
/// <typeparam name="TEnum">The Enum type.</typeparam>
public record BaseEnumFilterOperations<TEnum> where TEnum : struct, Enum
{
    /// <summary>
    /// The value for an equality filter.
    /// </summary>
    public TEnum? EqualsTo { get; init; }

    /// <summary>
    /// The value for a not-equal filter.
    /// </summary>
    public TEnum? NotEqualsTo { get; init; }

    /// <summary>
    /// A list of values for an 'in' filter.
    /// </summary>
    public TEnum[]? In { get; init; }

    /// <summary>
    /// A list of values for a 'not-in' filter.
    /// </summary>
    public TEnum[]? NotIn { get; init; }
}
