using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "StandaloneProduct")]
[Table("Products")]
public abstract class StandaloneProduct : CommonProduct
{
    [Attr]
    public int? TrialDuration { get; set; }

    [Attr]
    public int? TrialNo { get; set; }

    [Attr]
    public bool AllowsCustomEndDate { get; set; }

    [Attr(Capabilities = AttrCapabilities.None)]
    public override string SupportOptions { get; set; }

    [Attr(Capabilities = AttrCapabilities.None)]
    public override int? ScreenshotLinkEnabled { get; set; }

}
