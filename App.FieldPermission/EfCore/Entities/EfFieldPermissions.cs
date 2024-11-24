using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using MicroOrm.Dapper.Repositories.Attributes;

namespace EfCore.Entities;


[Table("FieldPermissions")]
public class EfFieldPermissions
{
    [Key, Identity]
    public Guid Id { get; set; }

    public int? RoleId { get; set; }
    
    public int? UserId { get; set; }
    
    public bool IsCanView { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string EntityName { get; set; } = null!;

    [Column(TypeName = "nvarchar(150)")]
    public string FieldName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedTime { get; set; }
}