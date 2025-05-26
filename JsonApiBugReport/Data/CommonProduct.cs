using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "CommonProduct")]
[Table("Products")]
public abstract class CommonProduct : ProductBase
{
    [Attr]
    [Required]
    public string UniqueCode { get; set; }

    [Attr]
    [Required]
    public int IsTaxable { get; set; }


    #region Product Group

    public Guid? ProductGroupId { get; set; }

    [HasOne]
    [ForeignKey(nameof(ProductGroupId))]
    public virtual ProductGroup ProductGroup { get; set; }

    #endregion Product Group
}
