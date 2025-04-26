using Nzr.QRest.Demo.Models.Entities;
using Nzr.QRest.Filtering;
using Nzr.QRest.Filtering.NumericOperations;
using Nzr.QRest.Filtering.StringOperations;

namespace Nzr.QRest.Demo.Controllers.QueryInput;

public record ProductFilterInput : FilterInput<Product>
{
    public StringFilterOperations? Name { get; init; }
    public DoubleFilterOperations? Price { get; init; }
}
