using Nzr.QRest.Sorting;
using Nzr.QRest.Tests.Models;

namespace Nzr.QRest.Tests.QueryInput;

public record TestEntitySortInput : SortInput<TestEntity>
{
    public SortDirection? AString { get; init; }
    public SortDirection? ANullableString { get; init; }

    public SortDirection? AInt { get; init; }
    public SortDirection? ANullableInt { get; init; }
    public SortDirection? ALong { get; init; }
    public SortDirection? ANullableLong { get; init; }
    public SortDirection? ADecimal { get; init; }
    public SortDirection? ANullableDecimal { get; init; }
    public SortDirection? ADouble { get; init; }
    public SortDirection? ANullableDouble { get; init; }
    public SortDirection? ABoolean { get; init; }
    public SortDirection? ANullableBoolean { get; init; }
    public SortDirection? AEnum { get; init; }
    public SortDirection? ANullableEnum { get; init; }
    public SortDirection? ADateTime { get; init; }
    public SortDirection? ANullableDateTime { get; init; }
    public SortDirection? ADateTimeOffset { get; init; }
    public SortDirection? ANullableDateTimeOffset { get; init; }
}
