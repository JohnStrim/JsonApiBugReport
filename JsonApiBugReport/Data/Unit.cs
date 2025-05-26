using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using JsonApiDotNetCore.Resources;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "Unit")]
[Table("Units")]
public class Unit :
    Identifiable<Guid>
{
    [Attr]
    public string Name { get; set; }

    [Attr]
    public string Mnemonic { get; set; }

    [Attr]
    [Required]
    [Precision(32, 10)]
    public decimal Quantity { get; set; }

    [Attr]
    public int? Duration { get; set; }

    [Attr]
    public bool IsActive { get; set; }

    public int ParentId { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(ParentId))]
    public virtual UnitGroup Parent { get; set; }
}
