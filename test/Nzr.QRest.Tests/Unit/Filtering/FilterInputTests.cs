using System.Globalization;
using FluentAssertions;
using Nzr.QRest.Filtering.BooleanOperations;
using Nzr.QRest.Filtering.DateTimeOperations;
using Nzr.QRest.Filtering.EnumOperations;
using Nzr.QRest.Filtering.NumericOperations;
using Nzr.QRest.Filtering.StringOperations;
using Nzr.QRest.Tests.Models;
using Nzr.QRest.Tests.QueryInput;
using Nzr.Snapshot.Xunit.Extensions;

namespace Nzr.QRest.Tests.Unit.Filter;

public class FilterInputTests
{
    private static DateTimeOffset ParseDateTimeOffset(string dateTimeOffset)
    {
        return DateTimeOffset.Parse(dateTimeOffset, CultureInfo.InvariantCulture);
    }

    private static DateTime ParseDateTime(string dateTimeOffset)
    {
        return DateTime.Parse(dateTimeOffset, CultureInfo.InvariantCulture);
    }

    private readonly List<TestEntity> _entities =
    [
        new TestEntity(1, 20, 1, 20, 1, 20, 19.99, 29.99, 19.99M, 29.99M, "abc", "def 123", Status.Active, Status.Inactive, true, false, ParseDateTimeOffset("2024-09-01 01:00:00 +02:00"), ParseDateTimeOffset("2024-09-01 01:00:00 +02:00"), ParseDateTime("2024-09-01 01:00:00"), ParseDateTime("2024-09-01 01:00:00")),
        new TestEntity(2, null, 2, null, 2, null, 29.99, null, 29.99M, null, "abc xyz", null, Status.Active, null, true, null, ParseDateTimeOffset("2024-09-02 01:00:00 +02:00"), null, ParseDateTime("2024-09-02 01:00:00"), null),

        new TestEntity(3, 40, 3, 40, 3, 40, 39.99, 59.99, 79.99M, 99.99M, "def", "def def", Status.New, Status.Active, false, true, ParseDateTimeOffset("2024-09-03 01:00:00 +02:00"), ParseDateTimeOffset("2024-09-03 01:00:00 +02:00"), ParseDateTime("2024-09-03 01:00:00"), ParseDateTime("2024-09-03 01:00:00")),
        new TestEntity(4, null, 4, null, 4, null, 59.99, null, 99.99M, null, "abc xyz", null, Status.Active, null, true, null, ParseDateTimeOffset("2024-09-04 23:59:59 +02:00"), null, ParseDateTime("2024-09-04 23:59:59"), null),

        new TestEntity(5, null, 5, null, 5, null, 49.99, null, 99.99M, null, "klm opq", null, Status.Active, null, true, null, ParseDateTimeOffset("2024-11-25 14:00:00 +02:00"), null, ParseDateTime("2024-11-25 14:00:00"), null),
    ];

    #region String Filter Operations

    [SnapshotFolder("StringFilterOperations")]
    [Theory]
    [MemberData(nameof(StringFilterTestData))]
    public void StringFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> StringFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.EqualsTo)} abc",
                new TestEntityFilterInput { AString = new StringFilterOperations { EqualsTo = "abc" } }
            },
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.NotEqualsTo)} abc",
                new TestEntityFilterInput { AString = new StringFilterOperations { NotEqualsTo = "abc" } }
            },
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.Contains)} abc",
                new TestEntityFilterInput { AString = new StringFilterOperations { Contains = "abc" } }
            },
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.DoesNotContain)} abc",
                new TestEntityFilterInput { AString = new StringFilterOperations { DoesNotContain = "abc" } }
            },
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.StartsWith)} def",
                new TestEntityFilterInput { AString = new StringFilterOperations { StartsWith = "def" } }
            },
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.EndsWith)} xyz",
                new TestEntityFilterInput { AString = new StringFilterOperations { EndsWith = "xyz" } }
            },
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.In)} [abc def]",
                new TestEntityFilterInput { AString = new StringFilterOperations { In = ["abc", "def"] } }
            },
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.NotIn)} [abc def]",
                new TestEntityFilterInput { AString = new StringFilterOperations { NotIn = ["abc", "def"] } }
            },
            {
                $"{nameof(TestEntityFilterInput.AString)} {nameof(StringFilterOperations.StartsWith)} abc and {nameof(StringFilterOperations.EndsWith)} xyz",
                new TestEntityFilterInput {
                    AString = new StringFilterOperations {
                        StartsWith = "abc",
                        EndsWith = "xyz"
                    }
                }
            }
        };

        return data;
    }

    [SnapshotFolder("NullableStringFilterOperations")]
    [Theory]
    [MemberData(nameof(NullableStringFilterTestData))]
    public void NullableStringFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> NullableStringFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.EqualsTo)} abc",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { EqualsTo = "abc" } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.NotEqualsTo)} abc",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { NotEqualsTo = "abc" } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.Contains)} abc",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { Contains = "abc" } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.DoesNotContain)} abc",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { DoesNotContain = "abc" } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.StartsWith)} def",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { StartsWith = "def" } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.EndsWith)} xyz",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { EndsWith = "xyz" } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.In)} [abc def]",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { In = ["abc", "def"] } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.NotIn)} [abc def]",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { NotIn = ["abc", "def"] } }
            },
                        {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.IsNull)}",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { IsNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.IsNotNull)}",
                new TestEntityFilterInput { ANullableString = new NullableStringFilterOperations { IsNotNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableString)} {nameof(NullableStringFilterOperations.StartsWith)} abc and {nameof(NullableStringFilterOperations.EndsWith)} xyz",
                new TestEntityFilterInput {
                    ANullableString = new NullableStringFilterOperations {
                        StartsWith = "abc",
                        EndsWith = "xyz"
                    }
                }
            }
        };

        return data;
    }

    #endregion

    #region Int Filter Operations

    [SnapshotFolder("IntFilterOperations")]
    [Theory]
    [MemberData(nameof(IntFilterTestData))]
    public void IntFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange

        var queryable = _entities.AsQueryable();

        // Act

        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert

        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> IntFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.EqualsTo)} 1",
                new TestEntityFilterInput { AInt = new IntFilterOperations { EqualsTo = 1 } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.NotEqualsTo)} 1",
                new TestEntityFilterInput { AInt = new IntFilterOperations { NotEqualsTo = 1 } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.GreaterThan)} 2",
                new TestEntityFilterInput { AInt = new IntFilterOperations { GreaterThan = 5 } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.GreaterThanOrEqual)} 2",
                new TestEntityFilterInput { AInt = new IntFilterOperations { GreaterThanOrEqual = 5 } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.LessThan)} 4",
                new TestEntityFilterInput { AInt = new IntFilterOperations { LessThan = 5 } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.LessThanOrEqual)} 4",
                new TestEntityFilterInput { AInt = new IntFilterOperations { LessThanOrEqual = 5 } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.In)} [1 3 5]",
                new TestEntityFilterInput { AInt = new IntFilterOperations { In = [1, 3, 5] } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.NotIn)} [1 3 5]",
                new TestEntityFilterInput { AInt = new IntFilterOperations { NotIn = [1, 3, 5] } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} {nameof(IntFilterOperations.From)} 3 {nameof(IntFilterOperations.To)} 6",
                new TestEntityFilterInput { AInt = new IntFilterOperations { From = 3, To = 6 } }
            },
            {
                $"{nameof(TestEntityFilterInput.AInt)} Combined [{nameof(IntFilterOperations.GreaterThan)} 2 and {nameof(IntFilterOperations.LessThan)} 5]",
                new TestEntityFilterInput { AInt = new IntFilterOperations { GreaterThan = 2, LessThan = 5 } }
            }
        };

        return data;
    }

    #endregion

    #region Nullable Int Filter Operations

    [SnapshotFolder("NullableIntFilterOperations")]
    [Theory]
    [MemberData(nameof(NullableIntFilterTestData))]
    public void NullableIntFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange

        var queryable = _entities.AsQueryable();

        // Act

        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert

        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> NullableIntFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.EqualsTo)} 1",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { EqualsTo = 1 } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.NotEqualsTo)} 1",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { NotEqualsTo = 1 } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.GreaterThan)} 2",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { GreaterThan = 5 } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.GreaterThanOrEqual)} 2",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { GreaterThanOrEqual = 5 } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.LessThan)} 4",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { LessThan = 5 } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.LessThanOrEqual)} 4",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { LessThanOrEqual = 5 } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.In)} [1 3 5]",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { In = [1, 3, 5] } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.NotIn)} [1 3 5]",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { NotIn = [1, 3, 5] } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} {nameof(NullableIntFilterOperations.From)} 3 {nameof(NullableIntFilterOperations.To)} 6",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { From = 3, To = 6 } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableIntFilterOperations.IsNull)}",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { IsNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableIntFilterOperations.IsNotNull)}",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { IsNotNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableInt)} Combined [{nameof(NullableIntFilterOperations.GreaterThan)} 2 and {nameof(NullableIntFilterOperations.LessThan)} 5]",
                new TestEntityFilterInput { ANullableInt = new NullableIntFilterOperations { GreaterThan = 2, LessThan = 5 } }
            }
        };

        return data;
    }

    #endregion

    #region Decimal Filter Operations

    [SnapshotFolder("DecimalFilterOperations")]
    [Theory]
    [MemberData(nameof(DecimalFilterTestData))]
    public void DecimalFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> DecimalFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.EqualsTo)} 19.99",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { EqualsTo = 199.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.NotEqualsTo)} 19.99",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { NotEqualsTo = 199.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.GreaterThan)} 19.99",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { GreaterThan = 299.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.GreaterThanOrEqual)} 19.99",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { GreaterThanOrEqual = 299.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.LessThan)} 59.99",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { LessThan = 299.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.LessThanOrEqual)} 59.99",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { LessThanOrEqual = 299.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.In)} [19.99 99.99]",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { In = [19.99M, 99.99M] } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.NotIn)} [19.99 99.99]",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { NotIn = [19.99M, 99.99M] } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} {nameof(DecimalFilterOperations.From)} 20 {nameof(DecimalFilterOperations.To)} 80",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { From = 20m, To = 80m } }
            },
            {
                $"{nameof(TestEntityFilterInput.ADecimal)} Combined [{nameof(DecimalFilterOperations.GreaterThan)} 19.99 and {nameof(DecimalFilterOperations.LessThan)} 59.99]",
                new TestEntityFilterInput { ADecimal = new DecimalFilterOperations { GreaterThan = 19.99M, LessThan = 59.99M } }
            }
        };

        return data;
    }

    [SnapshotFolder("NullableDecimalFilterOperations")]
    [Theory]
    [MemberData(nameof(NullableDecimalFilterTestData))]
    public void NullableDecimalFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> NullableDecimalFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.EqualsTo)} 19.99",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { EqualsTo = 199.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.NotEqualsTo)} 19.99",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { NotEqualsTo = 199.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.GreaterThan)} 19.99",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { GreaterThan = 299.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.GreaterThanOrEqual)} 19.99",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { GreaterThanOrEqual = 299.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.LessThan)} 59.99",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { LessThan = 299.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.LessThanOrEqual)} 59.99",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { LessThanOrEqual = 299.99M } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.In)} [19.99 99.99]",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { In = [19.99M, 99.99M] } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.NotIn)} [19.99 99.99]",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { NotIn = [19.99M, 99.99M] } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.From)} 20 {nameof(NullableDecimalFilterOperations.To)} 80",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { From = 20m, To = 80m } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.IsNull)}",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { IsNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} {nameof(NullableDecimalFilterOperations.IsNotNull)}",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { IsNotNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDecimal)} Combined [{nameof(NullableDecimalFilterOperations.GreaterThan)} 19.99 and {nameof(NullableDecimalFilterOperations.LessThan)} 59.99]",
                new TestEntityFilterInput { ANullableDecimal = new NullableDecimalFilterOperations { GreaterThan = 19.99M, LessThan = 59.99M } }
            }
        };

        return data;
    }


    #endregion

    #region DateTime Filter Operations

    [SnapshotFolder("DateTimeFilterOperations")]
    [Theory]
    [MemberData(nameof(DateTimeOffsetFilterTestData))]
    public void DateTimeOffisetFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> DateTimeOffsetFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.EqualsTo)} 2024-09-01 01:00:00 +02:00",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    EqualsTo = ParseDateTimeOffset("2024-09-01 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.NotEqualsTo)} 2024-09-01 01:00:00 +02:00",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    NotEqualsTo = ParseDateTimeOffset("2024-09-01 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.Before)} 2024-09-02 01:00:00 +02:00",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    Before = ParseDateTimeOffset("2024-09-02 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.BeforeOrEqual)} 2024-09-02 01:00:00 +02:00",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    BeforeOrEqual = ParseDateTimeOffset("2024-09-02 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.After)} 2024-09-03 01:00:00 +02:00",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    After = ParseDateTimeOffset("2024-09-03 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.AfterOrEqual)} 2024-09-03 01:00:00 +02:00",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    AfterOrEqual = ParseDateTimeOffset("2024-09-03 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.From)} 2024-09-02 01:00:00 +02:00 {nameof(DateTimeFilterOperations.To)} 2024-09-03 01:00:00 +02:00",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    From = ParseDateTimeOffset("2024-09-02 01:00:00 +02:00"),
                    To = ParseDateTimeOffset("2024-09-03 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.DateEquals)} 2024-09-02",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    DateEquals = DateOnly.Parse("2024-09-02", CultureInfo.InvariantCulture)
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.TimeEquals)} 01:00:00",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    TimeEquals = TimeOnly.Parse("01:00:00", CultureInfo.InvariantCulture)
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.YearEquals)} 2024",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    YearEquals = 2024
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.MonthEquals)} 9",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    MonthEquals = 9
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} {nameof(DateTimeFilterOperations.DayEquals)} 3",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    DayEquals = 3
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ADateTimeOffset)} Combined [{nameof(DateTimeFilterOperations.After)} 2024-09-01 01:00:00 +02:00 and {nameof(DateTimeFilterOperations.Before)} 2024-09-07 11:33:00 +02:00]",
                new TestEntityFilterInput { ADateTimeOffset = new DateTimeOffsetFilterOperations {
                    After = ParseDateTimeOffset("2024-09-01 01:00:00 +02:00"),
                    Before = ParseDateTimeOffset("2024-09-07 11:33:00 +02:00")
                }}
            }
        };

        return data;
    }

    [SnapshotFolder("NullableDateTimeFilterOperations")]
    [Theory]
    [MemberData(nameof(NullableDateTimeOffsetFilterTestData))]
    public void NullableDateTimeOffisetFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> NullableDateTimeOffsetFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.EqualsTo)} 2024-09-01 01:00:00 +02:00",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    EqualsTo = ParseDateTimeOffset("2024-09-01 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.NotEqualsTo)} 2024-09-01 01:00:00 +02:00",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    NotEqualsTo = ParseDateTimeOffset("2024-09-01 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.Before)} 2024-09-02 01:00:00 +02:00",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    Before = ParseDateTimeOffset("2024-09-02 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.BeforeOrEqual)} 2024-09-02 01:00:00 +02:00",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    BeforeOrEqual = ParseDateTimeOffset("2024-09-02 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.After)} 2024-09-03 01:00:00 +02:00",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    After = ParseDateTimeOffset("2024-09-03 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.AfterOrEqual)} 2024-09-03 01:00:00 +02:00",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    AfterOrEqual = ParseDateTimeOffset("2024-09-03 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.From)} 2024-09-02 01:00:00 +02:00 {nameof(NullableDateTimeOffsetFilterOperations.To)} 2024-09-03 01:00:00 +02:00",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    From = ParseDateTimeOffset("2024-09-02 01:00:00 +02:00"),
                    To = ParseDateTimeOffset("2024-09-03 01:00:00 +02:00")
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.DateEquals)} 2024-09-02",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    DateEquals = DateOnly.Parse("2024-09-02", CultureInfo.InvariantCulture)
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.TimeEquals)} 01:00:00",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    TimeEquals = TimeOnly.Parse("01:00:00", CultureInfo.InvariantCulture)
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.YearEquals)} 2024",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    YearEquals = 2024
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.MonthEquals)} 9",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    MonthEquals = 9
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.DayEquals)} 3",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    DayEquals = 3
                }}
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.IsNull)}",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations { IsNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableDateTimeOffsetFilterOperations.IsNotNull)}",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations { IsNotNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} Combined [{nameof(DateTimeFilterOperations.After)} 2024-09-01 01:00:00 +02:00 and {nameof(DateTimeFilterOperations.Before)} 2024-09-07 11:33:00 +02:00]",
                new TestEntityFilterInput { ANullableDateTimeOffset = new NullableDateTimeOffsetFilterOperations {
                    After = ParseDateTimeOffset("2024-09-01 01:00:00 +02:00"),
                    Before = ParseDateTimeOffset("2024-09-07 11:33:00 +02:00")
                }}
            }
        };

        return data;
    }

    #endregion

    #region Boolean Filter Operations

    [SnapshotFolder("BooleanFilterOperations")]
    [Theory]
    [MemberData(nameof(BooleanFilterTestData))]
    public void BooleanFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> BooleanFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ABoolean)} {nameof(BooleanFilterOperations.EqualsTo)} true",
                new TestEntityFilterInput { ABoolean = new BooleanFilterOperations { EqualsTo = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ABoolean)} {nameof(BooleanFilterOperations.EqualsTo)} false",
                new TestEntityFilterInput { ABoolean = new BooleanFilterOperations { EqualsTo = false } }
            },
            {
                $"{nameof(TestEntityFilterInput.ABoolean)} {nameof(BooleanFilterOperations.NotEqualsTo)} true",
                new TestEntityFilterInput { ABoolean = new BooleanFilterOperations { NotEqualsTo = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ABoolean)} {nameof(BooleanFilterOperations.NotEqualsTo)} false",
                new TestEntityFilterInput { ABoolean = new BooleanFilterOperations { NotEqualsTo = false } }
            }
        };

        return data;
    }


    [SnapshotFolder("NullableBooleanFilterOperations")]
    [Theory]
    [MemberData(nameof(NullableBooleanFilterTestData))]
    public void NullableBooleanFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> NullableBooleanFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ANullableBoolean)} {nameof(NullableBooleanFilterOperations.EqualsTo)} true",
                new TestEntityFilterInput { ANullableBoolean = new NullableBooleanFilterOperations { EqualsTo = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableBoolean)} {nameof(NullableBooleanFilterOperations.EqualsTo)} false",
                new TestEntityFilterInput { ANullableBoolean = new NullableBooleanFilterOperations { EqualsTo = false } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableBoolean)} {nameof(NullableBooleanFilterOperations.NotEqualsTo)} true",
                new TestEntityFilterInput { ANullableBoolean = new NullableBooleanFilterOperations { NotEqualsTo = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableBoolean)} {nameof(NullableBooleanFilterOperations.NotEqualsTo)} false",
                new TestEntityFilterInput { ANullableBoolean = new NullableBooleanFilterOperations { NotEqualsTo = false } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableBooleanFilterOperations.IsNull)}",
                new TestEntityFilterInput { ANullableBoolean = new NullableBooleanFilterOperations { IsNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableBooleanFilterOperations.IsNotNull)}",
                new TestEntityFilterInput { ANullableBoolean = new NullableBooleanFilterOperations { IsNotNull = true } }
            },
        };

        return data;
    }

    #endregion

    #region Enum Filter Operations

    [SnapshotFolder("EnumFilterOperations")]
    [Theory]
    [MemberData(nameof(EnumFilterTestData))]
    public void EnumFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> EnumFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.AEnum)} {nameof(EnumFilterOperations<Status>.EqualsTo)} Active",
                new TestEntityFilterInput { AEnum = new EnumFilterOperations<Status> { EqualsTo = Status.Active } }
            },
            {
                $"{nameof(TestEntityFilterInput.AEnum)} {nameof(EnumFilterOperations<Status>.EqualsTo)} Inactive",
                new TestEntityFilterInput { AEnum = new EnumFilterOperations<Status> { EqualsTo = Status.Inactive } }
            },
            {
                $"{nameof(TestEntityFilterInput.AEnum)} {nameof(EnumFilterOperations<Status>.NotEqualsTo)} Active",
                new TestEntityFilterInput { AEnum = new EnumFilterOperations<Status> { NotEqualsTo = Status.Active } }
            },
            {
                $"{nameof(TestEntityFilterInput.AEnum)} {nameof(EnumFilterOperations<Status>.NotEqualsTo)} Inactive",
                new TestEntityFilterInput { AEnum = new EnumFilterOperations<Status> { NotEqualsTo = Status.Inactive } }
            }
        };

        return data;
    }

    [SnapshotFolder("NullableEnumFilterOperations")]
    [Theory]
    [MemberData(nameof(NullableEnumFilterTestData))]
    public void NullableEnumFilterOperations_ToExpression_Should_Create_Correct_Filter_Expression(string testName, TestEntityFilterInput filter)
    {
        // Arrange
        var queryable = _entities.AsQueryable();

        // Act
        var result = queryable.Where(filter.ToExpression()).ToList();

        // Assert
        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntityFilterInput> NullableEnumFilterTestData()
    {
        var data = new TheoryData<string, TestEntityFilterInput>
        {
            {
                $"{nameof(TestEntityFilterInput.ANullableEnum)} {nameof(NullableEnumFilterOperations<Status>.EqualsTo)} Active",
                new TestEntityFilterInput { ANullableEnum = new NullableEnumFilterOperations<Status> { EqualsTo =Status.Active } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableEnum)} {nameof(NullableEnumFilterOperations<Status>.EqualsTo)} Inactive",
                new TestEntityFilterInput { ANullableEnum = new NullableEnumFilterOperations<Status> { EqualsTo = Status.Inactive } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableEnum)} {nameof(NullableEnumFilterOperations<Status>.NotEqualsTo)} Active",
                new TestEntityFilterInput { ANullableEnum = new NullableEnumFilterOperations<Status> { NotEqualsTo = Status.Active } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableEnum)} {nameof(NullableEnumFilterOperations<Status>.NotEqualsTo)} Inactive",
                new TestEntityFilterInput { ANullableEnum = new NullableEnumFilterOperations<Status> { NotEqualsTo = Status.Inactive } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableEnumFilterOperations<Status>.IsNull)}",
                new TestEntityFilterInput { ANullableEnum = new NullableEnumFilterOperations<Status> { IsNull = true } }
            },
            {
                $"{nameof(TestEntityFilterInput.ANullableDateTimeOffset)} {nameof(NullableEnumFilterOperations<Status>.IsNotNull)}",
                new TestEntityFilterInput { ANullableEnum = new NullableEnumFilterOperations<Status> { IsNotNull = true } }
            },
        };

        return data;
    }

    #endregion
}
