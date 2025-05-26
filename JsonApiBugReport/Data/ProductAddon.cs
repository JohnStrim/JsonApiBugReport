using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using JsonApiBugReport.Data.Enums;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "Addon")]
[Table("Products")]
public class ProductAddon : StandaloneProduct
{

    public ProductAddon()
    {
        ClassDiscriminator = ProductClassType.AddOn;
    }

    [Attr(Capabilities = AttrCapabilities.None)]
    public override string SupportOptions { get; set; }

    [Attr(Capabilities = AttrCapabilities.None)]
    public override int? ScreenshotLinkEnabled { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(CopyProductId))]
    public virtual ProductAddon CopyOf { get; set; }
}
