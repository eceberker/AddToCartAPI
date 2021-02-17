namespace AddToCartAPI.Model.Models
{
    public class CartProduct
    {
        public CartProduct()
        {
            Product = new Product();
        }
        public long Id { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public long CartId { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
    }
}
