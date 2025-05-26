using JsonApiBugReport.Data.Enums;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "ProductBase")]
[Table("Products")]
public abstract class ProductBase : Identifiable<Guid>
{
    [Attr]
    [Required]
    public virtual string Name { get; set; }

    [Attr]
    public bool? IsEnabled { get; set; }

    [Attr]
    public string ShortDescription { get; set; }

    [Attr]
    public string FullDescription { get; set; }

    [Attr]
    public bool AllowOrder { get; set; }

    [Attr]
    public virtual string SupportOptions { get; set; }

    [Attr]
    public virtual int? ScreenshotLinkEnabled { get; set; }

    [Attr]
    public bool? IsNew { get; set; }

    public int? UnitGroupId { get; set; }

    public Guid? CopyProductId { get; set; }

    [HasOne]
    [ForeignKey(nameof(UnitGroupId))]
    public virtual UnitGroup UnitGroup { get; set; }

    public Guid? PriceGroupId { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(PriceGroupId))]
    public virtual PriceGroup PriceGroup { get; set; }

    [Attr(Capabilities = AttrCapabilities.AllowView | AttrCapabilities.AllowFilter | AttrCapabilities.AllowSort)]
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    [Attr(Capabilities = AttrCapabilities.AllowView | AttrCapabilities.AllowFilter | AttrCapabilities.AllowSort)]
    public DateTimeOffset UpdatedAt { get; set; }

    public int? CreatedById { get; set; }

    public int? UpdatedById { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(CreatedById))]
    public virtual User CreatedBy { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(UpdatedById))]
    public virtual User UpdatedBy { get; set; }

    #region Discriminator Fields

    [Column("IsAddon")]
    private bool _isAddon;

    [NotMapped]
    public bool IsAddon
    {
        get { return _isAddon; }
    }

    [Column("IsBundle")]
    private bool _isBundle;

    [NotMapped]
    public bool IsBundle
    {
        get { return _isBundle; }
    }

    [Column("IsProductGroup")]
    private bool _isProductGroup;

    [NotMapped]
    public bool IsProductGroup
    {
        get { return _isProductGroup; }
    }

    [Column("Discriminator")]
    public ProductClassType Discriminator { get; set; }

    [NotMapped]
    public virtual ProductClassType ClassDiscriminator
    {
        get { return Discriminator; }

        set
        {
            switch (value)
            {
                case ProductClassType.AddOn:
                    _isAddon = true;
                    break;
                case ProductClassType.ProductGroup:
                    _isProductGroup = true;
                    break;
                case ProductClassType.Bundle:
                    _isBundle = true;
                    break;
                default: // ProductClassType.Product
                    _isAddon = false;
                    _isProductGroup = false;
                    _isBundle = false;
                    break;
            }
            Discriminator = value;
        }
    }

    #endregion Discriminator Fields
}
