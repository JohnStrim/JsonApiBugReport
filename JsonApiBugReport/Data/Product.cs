using JsonApiBugReport.Data.Enums;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "Product")]
[Table("Products")]
public class Product : StandaloneProduct
{

    public Product()
    {
        ClassDiscriminator = ProductClassType.Product;
    }

    [Attr]
    public bool? SupportsMonthlyBillingFrequency { get; set; }

    [HasOne(Capabilities = HasOneCapabilities.AllowView | HasOneCapabilities.AllowInclude)]
    [ForeignKey(nameof(CopyProductId))]
    public virtual Product CopyOf { get; set; }

}
