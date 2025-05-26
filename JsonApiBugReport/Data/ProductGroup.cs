using JsonApiBugReport.Data.Enums;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "ProductGroup")]
[Table("Products")]
public class ProductGroup : ProductBase
{
    public ProductGroup()
    {
        ClassDiscriminator = ProductClassType.ProductGroup;
    }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(CopyProductId))]
    public virtual ProductGroup CopyOf { get; set; }

    [Attr]
    public int? IncludeAllProducts { get; set; }

    [HasMany(Capabilities = HasManyCapabilities.AllowView | HasManyCapabilities.AllowInclude)]
    public virtual ICollection<CommonProduct> Products { get; set; }
}
