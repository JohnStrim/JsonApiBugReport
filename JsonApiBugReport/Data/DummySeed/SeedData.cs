
using Bogus;
using JsonApiBugReport.Data;
using JsonApiBugReport.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiBugReport.Data.DummySeed;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if the database is already seeded
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            // Seed Users
            var userFaker = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.IsActive, f => f.Random.Bool())
                .RuleFor(u => u.Language, f => f.Random.Word())
                .RuleFor(u => u.IsSystemAdmin, f => f.Random.Bool())
                .RuleFor(u => u.IsApiUser, f => f.Random.Bool());

            var users = userFaker.Generate(100);
            context.Users.AddRange(users);

            // Seed UnitGroups
            var unitGroupFaker = new Faker<UnitGroup>()
                .RuleFor(ug => ug.Name, f => f.Commerce.Department())
                .RuleFor(ug => ug.Description, f => f.Lorem.Sentence())
                .RuleFor(ug => ug.IsActive, f => f.Random.Bool())
                .RuleFor(ug => ug.CreatedAt, f => f.Date.Past())
                .RuleFor(ug => ug.UpdatedAt, f => f.Date.Past())
                .RuleFor(ug => ug.CreatedBy, f => f.PickRandom(users))
                .RuleFor(ug => ug.UpdatedBy, f => f.PickRandom(users));

            var unitGroups = unitGroupFaker.Generate(50);
            context.UnitGroups.AddRange(unitGroups);

            // Seed Units
            var unitFaker = new Faker<Unit>()
                .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                .RuleFor(u => u.Mnemonic, f => f.Random.AlphaNumeric(5))
                .RuleFor(u => u.Quantity, f => f.Random.Decimal(1, 100))
                .RuleFor(u => u.Duration, f => f.Random.Int(1, 12))
                .RuleFor(u => u.IsActive, f => f.Random.Bool())
                .RuleFor(u => u.Parent, f => f.PickRandom(unitGroups));

            var units = unitFaker.Generate(200);
            context.Units.AddRange(units);

            // Seed PriceGroups
            var priceGroupFaker = new Faker<PriceGroup>()
                .RuleFor(pg => pg.Name, f => f.Commerce.Department())
                .RuleFor(pg => pg.Description, f => f.Lorem.Sentence())
                .RuleFor(pg => pg.CreatedAt, f => f.Date.Past())
                .RuleFor(pg => pg.UpdatedAt, f => f.Date.Past())
                .RuleFor(pg => pg.CreatedBy, f => f.PickRandom(users))
                .RuleFor(pg => pg.UpdatedBy, f => f.PickRandom(users));

            var priceGroups = priceGroupFaker.Generate(50);
            context.PriceGroups.AddRange(priceGroups);

            // Seed Products
            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.IsEnabled, f => f.Random.Bool())
                .RuleFor(p => p.ShortDescription, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.FullDescription, f => f.Lorem.Paragraph())
                .RuleFor(p => p.AllowOrder, f => f.Random.Bool())
                .RuleFor(p => p.SupportOptions, f => f.Lorem.Word())
                .RuleFor(p => p.ScreenshotLinkEnabled, f => f.Random.Int(0, 1))
                .RuleFor(p => p.IsNew, f => f.Random.Bool())
                .RuleFor(p => p.UnitGroup, f => f.PickRandom(unitGroups))
                .RuleFor(p => p.PriceGroup, f => f.PickRandom(priceGroups))
                .RuleFor(p => p.CreatedAt, f => f.Date.Past())
                .RuleFor(p => p.UpdatedAt, f => f.Date.Past())
                .RuleFor(p => p.CreatedBy, f => f.PickRandom(users))
                .RuleFor(p => p.UpdatedBy, f => f.PickRandom(users))
                .RuleFor(p => p.SupportsMonthlyBillingFrequency, f => f.Random.Bool());


            var products = productFaker.Generate(400);
            context.Products.AddRange(products);

            // Seed ProductAddons
            var productAddonFaker = new Faker<ProductAddon>()
                .RuleFor(pa => pa.Name, f => f.Commerce.ProductName())
                .RuleFor(pa => pa.IsEnabled, f => f.Random.Bool())
                .RuleFor(pa => pa.ShortDescription, f => f.Commerce.ProductDescription())
                .RuleFor(pa => pa.FullDescription, f => f.Lorem.Paragraph())
                .RuleFor(pa => pa.AllowOrder, f => f.Random.Bool())
                .RuleFor(pa => pa.SupportOptions, f => f.Lorem.Word())
                .RuleFor(pa => pa.ScreenshotLinkEnabled, f => f.Random.Int(0, 1))
                .RuleFor(pa => pa.IsNew, f => f.Random.Bool())
                .RuleFor(pa => pa.UnitGroup, f => f.PickRandom(unitGroups))
                .RuleFor(pa => pa.PriceGroup, f => f.PickRandom(priceGroups))
                .RuleFor(pa => pa.CreatedAt, f => f.Date.Past())
                .RuleFor(pa => pa.UpdatedAt, f => f.Date.Past())
                .RuleFor(pa => pa.CreatedBy, f => f.PickRandom(users))
                .RuleFor(pa => pa.UpdatedBy, f => f.PickRandom(users))
                .RuleFor(pa => pa.TrialDuration, f => f.Random.Int(1, 30))
                .RuleFor(pa => pa.TrialNo, f => f.Random.Int(1, 10))
                .RuleFor(pa => pa.AllowsCustomEndDate, f => f.Random.Bool());

            var productAddons = productAddonFaker.Generate(400);
            context.ProductAddons.AddRange(productAddons);

            // Seed ProductBundles
            var productBundleFaker = new Faker<ProductBundle>()
                .RuleFor(pb => pb.Name, f => f.Commerce.ProductName())
                .RuleFor(pb => pb.IsEnabled, f => f.Random.Bool())
                .RuleFor(pb => pb.ShortDescription, f => f.Commerce.ProductDescription())
                .RuleFor(pb => pb.FullDescription, f => f.Lorem.Paragraph())
                .RuleFor(pb => pb.AllowOrder, f => f.Random.Bool())
                .RuleFor(pb => pb.SupportOptions, f => f.Lorem.Word())
                .RuleFor(pb => pb.ScreenshotLinkEnabled, f => f.Random.Int(0, 1))
                .RuleFor(pb => pb.IsNew, f => f.Random.Bool())
                .RuleFor(pb => pb.UnitGroup, f => f.PickRandom(unitGroups))
                .RuleFor(pb => pb.PriceGroup, f => f.PickRandom(priceGroups))
                .RuleFor(pb => pb.CreatedAt, f => f.Date.Past())
                .RuleFor(pb => pb.UpdatedAt, f => f.Date.Past())
                .RuleFor(pb => pb.CreatedBy, f => f.PickRandom(users))
                .RuleFor(pb => pb.UpdatedBy, f => f.PickRandom(users));

            var productBundles = productBundleFaker.Generate(400);
            context.ProductBundles.AddRange(productBundles);

            // Seed ProductGroups
            var productGroupFaker = new Faker<ProductGroup>()
                .RuleFor(pg => pg.Name, f => f.Commerce.ProductName())
                .RuleFor(pg => pg.IsEnabled, f => f.Random.Bool())
                .RuleFor(pg => pg.ShortDescription, f => f.Commerce.ProductDescription())
                .RuleFor(pg => pg.FullDescription, f => f.Lorem.Paragraph())
                .RuleFor(pg => pg.AllowOrder, f => f.Random.Bool())
                .RuleFor(pg => pg.SupportOptions, f => f.Lorem.Word())
                .RuleFor(pg => pg.ScreenshotLinkEnabled, f => f.Random.Int(0, 1))
                .RuleFor(pg => pg.IsNew, f => f.Random.Bool())
                .RuleFor(pg => pg.UnitGroup, f => f.PickRandom(unitGroups))
                .RuleFor(pg => pg.PriceGroup, f => f.PickRandom(priceGroups))
                .RuleFor(pg => pg.CreatedAt, f => f.Date.Past())
                .RuleFor(pg => pg.UpdatedAt, f => f.Date.Past())
                .RuleFor(pg => pg.CreatedBy, f => f.PickRandom(users))
                .RuleFor(pg => pg.UpdatedBy, f => f.PickRandom(users))
                .RuleFor(pg => pg.IncludeAllProducts, f => f.Random.Int(0, 1));

            var productGroups = productGroupFaker.Generate(400);
            context.ProductGroups.AddRange(productGroups);

            // Save all changes to the database
            context.SaveChanges();
        }
    }
}
