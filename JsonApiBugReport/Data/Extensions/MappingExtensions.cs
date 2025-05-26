using JsonApiBugReport.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace JsonApiBugReport.Data.Extensions;

public static class MappingExtensions
{
    public static ModelBuilder AddProductMapping(this ModelBuilder builder)
    {
        #region ProductBase

        var productBaseBuilder = builder.Entity<ProductBase>();
        productBaseBuilder.HasDiscriminator(p => p.Discriminator)
            .HasValue<Product>(ProductClassType.Product)
            .HasValue<ProductAddon>(ProductClassType.AddOn)
            .HasValue<ProductGroup>(ProductClassType.ProductGroup)
            .HasValue<ProductBundle>(ProductClassType.Bundle);
        productBaseBuilder.Property(p => p.IsProductGroup)
            .HasConversion<int>();
        productBaseBuilder
            .ToTable(t => t.HasTrigger("TR_tblProduct_Insert"))
            .ToTable(t => t.HasTrigger("TR_tblProduct_Deleted"));
        productBaseBuilder
            .Property(p => p.IsBundle)
            .HasColumnName("IsBundle")
            .HasField("_isBundle");
        productBaseBuilder
            .Property(p => p.IsAddon)
            .HasColumnName("IsAddon")
            .HasField("_isAddon");
        productBaseBuilder
            .Property(p => p.IsProductGroup)
            .HasColumnName("IsProductGroup")
            .HasField("_isProductGroup");

        #endregion ProductBase




        return builder;
    }

    public static ModelBuilder AddAuditableMapping(this ModelBuilder builder)
    {

        builder.Entity<UnitGroup>()
         .HasOne(pg => pg.CreatedBy)
         .WithMany()
         .HasForeignKey(pg => pg.CreatedById)
         .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UnitGroup>()
         .HasOne(pg => pg.UpdatedBy)
         .WithMany()
         .HasForeignKey(pg => pg.UpdatedById)
         .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ProductBase>()
         .HasOne(pg => pg.CreatedBy)
         .WithMany()
         .HasForeignKey(pg => pg.CreatedById)
         .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ProductBase>()
         .HasOne(pg => pg.UpdatedBy)
         .WithMany()
         .HasForeignKey(pg => pg.UpdatedById)
         .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PriceGroup>()
         .HasOne(pg => pg.CreatedBy)
         .WithMany()
         .HasForeignKey(pg => pg.CreatedById)
         .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PriceGroup>()
         .HasOne(pg => pg.UpdatedBy)
         .WithMany()
         .HasForeignKey(pg => pg.UpdatedById)
         .OnDelete(DeleteBehavior.Restrict);

        return builder;
    }
}
