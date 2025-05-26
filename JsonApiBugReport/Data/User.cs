using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonApiBugReport.Data;

[Resource(GenerateControllerEndpoints = JsonApiEndpoints.None, PublicName = "User")]
[Table("Users")]
public class User : Identifiable<int>
{
    /// <summary>
    /// Get or set the User's first name.
    /// </summary>
    [Attr]
    public string FirstName { get; set; }

    /// <summary>
    /// Get or set the User's last name.
    /// </summary>
    [Attr]
    public string LastName { get; set; }

    /// <summary>
    /// Get or set the User's email.
    /// </summary>
    [Attr]
    public string Email { get; set; }

    /// <summary>
    /// User's active status.
    /// </summary>
    [Attr]
    public bool IsActive { get; set; }

    /// <summary>
    /// Get or set the User's language.
    /// </summary>
    [Attr]
    [Required]
    public string Language { get; set; }

    /// <summary>
    /// Indicates if the user is a system administrator.
    /// </summary>
    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public bool IsSystemAdmin { get; set; }

    /// <summary>
    /// Indicates if the user is an API user.
    /// </summary>
    public bool? IsApiUser { get; set; }

}
