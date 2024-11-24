using App.FieldPermission.Attributes;
namespace App.FieldPermission.Dtos;

public class ProductDtos
{
    public class Get
    {
        [FieldPermission("Id")]
        public int Id { get; set; }

        [FieldPermission("Name")]
        public required string Name { get; set; }

        [FieldPermission("Price")]
        public decimal Price { get; set; }
        
        [FieldPermission("Sku")]
        public required string Sku { get; set; }
        
        [FieldPermission("BasicUnit")]
        public int BasicUnit { get; set; }
        
        [FieldPermission("CreatedDate")]
        public DateTime CreatedDate { get; set; }
        
        [FieldPermission("UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}