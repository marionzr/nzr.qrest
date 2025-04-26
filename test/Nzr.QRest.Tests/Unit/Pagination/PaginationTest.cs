using System.Globalization;
using FluentAssertions;
using Nzr.QRest.Paging;
using Nzr.QRest.Sorting;
using Nzr.QRest.Tests.Models;
using Nzr.QRest.Tests.QueryInput;
using Nzr.Snapshot.Xunit.Extensions;

namespace Nzr.QRest.Tests.Unit.Pagination;

public class PaginationTest
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

    [SnapshotFolder("Pagination")]
    [Theory]
    [InlineData("Page 1, Page Size 2", 1, 2)]
    [InlineData("Page 1, Page Size 5", 1, 5)]
    [InlineData("Page 2, Page Size 3", 2, 3)]
    public void ToPagedResult_Should_Return_Correct_PagedResult(string testCase, int page, int pageSize)
    {
        // Arrange

        var sortInput = new TestEntitySortInput { AInt = SortDirection.ASC }; // To avoid flaky tests

        var queryable = _entities.AsQueryable();
        var pagination = new PaginationInput
        {
            Page = page,
            PageSize = pageSize,
        };

        // Act

        var result = sortInput
            .ApplySort(queryable)
            .ToPagedResult(pagination);

        // Assert

        result.Should().Match(testCase);
    }
}
