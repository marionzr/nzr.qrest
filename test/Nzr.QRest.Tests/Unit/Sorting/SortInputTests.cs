using System.Globalization;
using FluentAssertions;
using Nzr.QRest.Sorting;
using Nzr.QRest.Tests.Models;
using Nzr.QRest.Tests.QueryInput;
using Nzr.Snapshot.Xunit.Extensions;

public class SortInputTests
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

    [SnapshotFolder("SortOperations")]
    [Theory]
    [MemberData(nameof(SortTestData))]
    public void SortOperations_ToExpression_Should_Create_Correct_Sort_Expression(string testName, TestEntitySortInput sort)
    {
        // Arrange

        var queryable = _entities.AsQueryable();

        // Act

        var result = sort.ApplySort(queryable);

        // Assert

        result.Should().Match(testName);
    }

    public static TheoryData<string, TestEntitySortInput> SortTestData()
    {
        var data = new TheoryData<string, TestEntitySortInput>
        {
            {
                $"{nameof(TestEntitySortInput.ABoolean)} ASC",
                new TestEntitySortInput { ABoolean = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ABoolean)} DESC",
                new TestEntitySortInput { ABoolean = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ADateTime)} ASC",
                new TestEntitySortInput { ADateTime = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ADateTime)} DESC",
                new TestEntitySortInput { ADateTime = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ADateTimeOffset)} ASC",
                new TestEntitySortInput { ADateTimeOffset = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ADateTimeOffset)} DESC",
                new TestEntitySortInput { ADateTimeOffset = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ADouble)} ASC",
                new TestEntitySortInput { ADouble = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ADouble)} DESC",
                new TestEntitySortInput { ADouble = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ADecimal)} ASC",
                new TestEntitySortInput { ADecimal = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ADecimal)} DESC",
                new TestEntitySortInput { ADecimal = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.AInt)} ASC",
                new TestEntitySortInput { AInt = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.AInt)} DESC",
                new TestEntitySortInput { AInt = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ALong)} ASC",
                new TestEntitySortInput { ALong = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ALong)} DESC",
                new TestEntitySortInput { ALong = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.AEnum)} ASC",
                new TestEntitySortInput { AEnum = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.AEnum)} DESC",
                new TestEntitySortInput { AEnum = SortDirection.DESC }
            },

            {
                $"{nameof(TestEntitySortInput.ANullableBoolean)} ASC",
                new TestEntitySortInput { ANullableBoolean = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableBoolean)} DESC",
                new TestEntitySortInput { ANullableBoolean = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableDateTime)} ASC",
                new TestEntitySortInput { ANullableDateTime = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableDateTime)} DESC",
                new TestEntitySortInput { ANullableDateTime = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableDateTimeOffset)} ASC",
                new TestEntitySortInput { ANullableDateTimeOffset = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableDateTimeOffset)} DESC",
                new TestEntitySortInput { ANullableDateTimeOffset = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableDouble)} ASC",
                new TestEntitySortInput { ANullableDouble = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableDouble)} DESC",
                new TestEntitySortInput { ANullableDouble = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableDecimal)} ASC",
                new TestEntitySortInput { ANullableDecimal = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableDecimal)} DESC",
                new TestEntitySortInput { ANullableDecimal = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableInt)} ASC",
                new TestEntitySortInput { ANullableInt = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableInt)} DESC",
                new TestEntitySortInput { ANullableInt = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableLong)} ASC",
                new TestEntitySortInput { ANullableLong = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableLong)} DESC",
                new TestEntitySortInput { ANullableLong = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableEnum)} ASC",
                new TestEntitySortInput { ANullableEnum = SortDirection.ASC }
            },
            {
                $"{nameof(TestEntitySortInput.ANullableEnum)} DESC",
                new TestEntitySortInput { ANullableEnum = SortDirection.DESC }
            },
            {
                $"{nameof(TestEntitySortInput.AString)} ASC and {nameof(TestEntitySortInput.ADecimal)} DESC",
                new TestEntitySortInput {
                        AString = SortDirection.ASC,
                        ADecimal = SortDirection.DESC
                }
            }
        };

        return data;
    }

}
