using System.Collections.Generic;

namespace AddToCartAPI.Service.ViewModels
{
    public class ShoppingCartViewModel
    {
        public Result message { get; set; }
        public long CartId { get; set; }
        public List<CartProductViewModel> Items { get; set; } = new List<CartProductViewModel>();

    }
}
