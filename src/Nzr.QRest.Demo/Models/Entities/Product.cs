using Nzr.QRest.Demo.Models.Enums;

namespace Nzr.QRest.Demo.Models.Entities;

public class Product
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required ProductStatus Status { get; set; }
    public required double Price { get; set; }
    public required bool IsVirtual { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public required DateTimeOffset LastUpdateAt { get; set; }
}
