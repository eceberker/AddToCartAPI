using System.ComponentModel.DataAnnotations.Schema;

namespace AddToCartAPI.Model.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public long VariantId { get; set; }
        public long CategoryId { get; set; }
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
