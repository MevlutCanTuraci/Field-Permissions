using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EfCore.Entities;

[Table("Products")]
public class EfProducts
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    public required string Name { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public required string Sku { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "int")]
    public int BasicUnit { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }
    
    [Column(TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }
}