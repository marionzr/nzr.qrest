using Microsoft.EntityFrameworkCore;
using Nzr.QRest.Demo.Models.Entities;

namespace Nzr.QRest.Demo.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
}
