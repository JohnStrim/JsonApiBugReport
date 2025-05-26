using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using JsonApiDotNetCore.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.All, PublicName = "PriceGroup")]
[Table("PriceGroups")]
public class PriceGroup : Identifiable<Guid>
{
    [Attr]
    [Required]
    public string Name { get; set; }

    [Attr]
    public string Description { get; set; }

    [Attr]
    public bool IsDeleted { get; set; }

    [HasMany(Capabilities = HasManyCapabilities.AllowView | HasManyCapabilities.AllowInclude | HasManyCapabilities.AllowFilter)]
    public virtual ICollection<CommonProduct> Products { get; set; }

    public int CreatedById { get; set; }

    public int UpdatedById { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(CreatedById))]
    [Required]
    public virtual User CreatedBy { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(UpdatedById))]
    [Required]
    public virtual User UpdatedBy { get; set; }

    [Attr(Capabilities = AttrCapabilities.AllowView | AttrCapabilities.AllowFilter | AttrCapabilities.AllowSort)]
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    [Attr(Capabilities = AttrCapabilities.AllowView | AttrCapabilities.AllowFilter | AttrCapabilities.AllowSort)]
    [Required]
    public DateTimeOffset UpdatedAt { get; set; }
}
