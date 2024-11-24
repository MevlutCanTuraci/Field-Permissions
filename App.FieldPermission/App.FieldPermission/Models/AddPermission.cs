using System.ComponentModel;

namespace App.FieldPermission.Models;

public class AddPermission
{
    [DefaultValue("ProductDtos.Get")]
    public required string EntityName { get; set; }
    public required string FieldName { get; set; }
    public required bool IsCanView { get; set; }
    public int? UserId { get; set; }
    public int? RoleId { get; set; }
}