using Nzr.QRest.Filtering;
using Nzr.QRest.Filtering.BooleanOperations;
using Nzr.QRest.Filtering.DateTimeOperations;
using Nzr.QRest.Filtering.EnumOperations;
using Nzr.QRest.Filtering.NumericOperations;
using Nzr.QRest.Filtering.StringOperations;
using Nzr.QRest.Tests.Models;

namespace Nzr.QRest.Tests.QueryInput;

public record TestEntityFilterInput : FilterInput<TestEntity>
{
    public IntFilterOperations? AInt { get; init; }
    public NullableIntFilterOperations? ANullableInt { get; init; }
    public LongFilterOperations? ALong { get; init; }
    public NullableLongFilterOperations? ANullableLong { get; init; }
    public ShortFilterOperations? AShort { get; init; }
    public NullableShortFilterOperations? ANullableShort { get; init; }

    public FloatFilterOperations? AFloat { get; init; }
    public NullableFloatFilterOperations? ANullableFloat { get; init; }
    public DoubleFilterOperations? ADouble { get; init; }
    public NullableDoubleFilterOperations? ANullableDouble { get; init; }
    public DecimalFilterOperations? ADecimal { get; init; }
    public NullableDecimalFilterOperations? ANullableDecimal { get; init; }

    public StringFilterOperations? AString { get; init; }
    public NullableStringFilterOperations? ANullableString { get; init; }

    public EnumFilterOperations<Status>? AEnum { get; init; }
    public NullableEnumFilterOperations<Status>? ANullableEnum { get; init; }

    public BooleanFilterOperations? ABoolean { get; init; }
    public NullableBooleanFilterOperations? ANullableBoolean { get; init; }

    public DateTimeOffsetFilterOperations? ADateTimeOffset { get; init; }
    public NullableDateTimeOffsetFilterOperations? ANullableDateTimeOffset { get; init; }
    public DateTimeFilterOperations? ADateTime { get; init; }
    public NullableDateTimeFilterOperations? ANullableDateTime { get; init; }
}
