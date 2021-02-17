using AddToCartAPI.Model.Models;

namespace AddToCartAPI.Service.ViewModels
{
    public class CartProductViewModel
    {
        public Result Message { get; set; }
        public long ProductId { get; set; }
        public long CartId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public long VariantId { get; set; }
        public long CategoryId { get; set; }

        public static CartProductViewModel GetCartProductViewModel(CartProduct cartProduct)
        {
            return new CartProductViewModel
            {
                ProductId = cartProduct.Product.ProductId,
                ProductName = cartProduct.Product.ProductName,
                Quantity = cartProduct.Quantity,
                Price = cartProduct.Product.Price,
                VariantId = cartProduct.Product.VariantId,
                CategoryId = cartProduct.Product.CategoryId

            };
        }
    }
}
