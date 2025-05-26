using JsonApiBugReport.Data.Enums;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "Bundle")]
[Table("Products")]
public class ProductBundle : CommonProduct
{
    public ProductBundle()
    {
        ClassDiscriminator = ProductClassType.Bundle;
    }

    [Attr(Capabilities = AttrCapabilities.None)]
    public override string SupportOptions { get; set; }

    [Attr(Capabilities = AttrCapabilities.None)]
    public override int? ScreenshotLinkEnabled { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(CopyProductId))]
    public virtual ProductBundle CopyOf { get; set; }

}
