using AddToCartAPI.Service.ViewModels;
using System.Threading.Tasks;

namespace AddToCartAPI.Service.Interfaces
{
    public interface ICartService
    {
        Task<CartProductViewModel> AddProductToCart(CartProductViewModel cartItem);
        Task<ShoppingCartViewModel> GetCart(long cartId);

    }
}
