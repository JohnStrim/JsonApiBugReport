using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using JsonApiDotNetCore.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "UnitGroup")]
[Table("UnitGroups")]
public class UnitGroup : Identifiable<int>
{
    [Attr]
    [Required]
    public string Name { get; set; }

    [Attr]
    public string Description { get; set; }

    [Attr]
    [Required]
    public bool IsActive { get; set; }


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

    [HasMany(Capabilities = HasManyCapabilities.AllowView | HasManyCapabilities.AllowFilter | HasManyCapabilities.AllowInclude)]
    public virtual ICollection<Unit> Units { get; set; }
}
