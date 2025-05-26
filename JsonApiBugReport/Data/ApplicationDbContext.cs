using JsonApiBugReport.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JsonApiBugReport.Data;


public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ProductBase> ProductsBase { get; set; }
    public DbSet<CommonProduct> CommonProducts { get; set; }
    public DbSet<StandaloneProduct> StandaloneProducts { get; set; }
    public DbSet<ProductAddon> ProductAddons { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductBundle> ProductBundles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UnitGroup> UnitGroups { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PriceGroup> PriceGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddProductMapping()
            .AddAuditableMapping();
    }
}
