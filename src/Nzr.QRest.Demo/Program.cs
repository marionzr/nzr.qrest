using System.Text.Json.Serialization;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Nzr.QRest.Demo.Models;
using Nzr.QRest.Demo.Models.Entities;
using Nzr.QRest.Demo.Models.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder
    .Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=query-builder-demo.db"));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await db.Database.EnsureCreatedAsync();

if (!await db.Products.AnyAsync())
{
    var faker = new Faker<Product>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>())
        .RuleFor(p => p.Price, f => Convert.ToDouble(f.Commerce.Price(1, 1000)))
        .RuleFor(p => p.IsVirtual, f => f.Random.Bool())
        .RuleFor(p => p.CreatedAt, f => f.Date.PastOffset(1))
        .RuleFor(p => p.LastUpdateAt, f => f.Date.RecentOffset(30));

    db.Products.AddRange(faker.Generate(50));

    await db.SaveChangesAsync();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllers();

await app.RunAsync();
