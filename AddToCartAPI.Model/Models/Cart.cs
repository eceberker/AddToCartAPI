using System.Collections.Generic;

namespace AddToCartAPI.Model.Models
{
    public class Cart
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public IList<CartProduct> Items { get; set; } = new List<CartProduct>();
    }
}
