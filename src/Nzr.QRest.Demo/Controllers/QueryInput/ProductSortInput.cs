using Nzr.QRest.Demo.Models.Entities;
using Nzr.QRest.Sorting;

namespace Nzr.QRest.Demo.Controllers.QueryInput;

public record ProductSortInput : SortInput<Product>
{
    public SortDirection? Name { get; init; }
    public SortDirection? Price { get; init; }
}
