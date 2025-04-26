using Microsoft.AspNetCore.Mvc;
using Nzr.QRest.Demo.Controllers.QueryInput;
using Nzr.QRest.Demo.Models;
using Nzr.QRest.Demo.Models.Entities;
using Nzr.QRest.Paging;

namespace Nzr.QRest.Demo.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<Product>>> GetProducts(
        [FromQuery] ProductFilterInput? filter,
        [FromQuery] ProductSortInput? sort,
        [FromQuery] PaginationInput? pagination,
        CancellationToken cancellationToken)
    {
        var query = new QueryInput<Product, ProductFilterInput, ProductSortInput>
        {
            Filter = filter,
            Sort = sort,
            Pagination = pagination ?? new PaginationInput()
        };

        var result = await QueryProductsAsync(query, cancellationToken);

        return result;
    }

    private async Task<PagedResult<Product>> QueryProductsAsync(QueryInput<Product, ProductFilterInput, ProductSortInput> query, CancellationToken cancellationToken)
    {
        var queryable = _context.Products.AsQueryable();

        if (query.Filter != null)
        {
            queryable = queryable.Where(query.Filter.ToExpression());
        }

        if (query.Sort != null)
        {
            queryable = query.Sort.ApplySort(queryable);
        }

        return await queryable.ToPagedResultAsync(
            query.Pagination ?? new PaginationInput(),
            cancellationToken);
    }
}
