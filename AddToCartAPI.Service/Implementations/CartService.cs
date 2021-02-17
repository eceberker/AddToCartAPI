using AddToCartAPI.Model.Models;
using AddToCartAPI.Model.Repository;
using AddToCartAPI.Service.Interfaces;
using AddToCartAPI.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddToCartAPI.Service.Implementations
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _repository;
        private readonly IProductService _productRepository;
        public CartService(IRepository<Cart> repository, IProductService productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }
        public async Task<CartProductViewModel> AddProductToCart(CartProductViewModel cartItem)
        {
            //Gets cart from Db
            var cart = await GetCartFrDb(cartItem.CartId);

            //If cart is not exist, create new cart
            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = cartItem.CartId

                };

                _repository.Add(cart);

            }

            // Model to be returned
            var returnModel = new CartProductViewModel()
            {
                Message = new Result()
            };

            //If product already exists in cart
            var cartItemDb = cart.Items.FirstOrDefault(x => x.ProductId == cartItem.ProductId);

            //Retrieve product from Db
            var product = await _productRepository.GetProductAsync(cartItem.ProductId);

            // If product not found /or does not exist 
            if(product == null)
            {
                returnModel.Message.status = "error";
                returnModel.Message.text = "Product not found";
                return returnModel;
            }

            //If not exists in cart
            if (cartItemDb == null)
            {

                //Update product stock
                product.Stock -= cartItem.Quantity;

                cartItemDb = new CartProduct
                {
                    Cart = cart,
                    Quantity = cartItem.Quantity,
                    ProductId = cartItem.ProductId,
                    Product = product
                };

                cart.Items.Add(cartItemDb);
            }
            else
            {
                // Stock control -- returns error if product is out of stock
                if (product.Stock - cartItem.Quantity < 0)
                {
                    returnModel.Message.status = "error";
                    returnModel.Message.text = $"Remaining stock for product is {cart.Items.FirstOrDefault(x => x.ProductId == cartItem.ProductId).Product.Stock}.";
                    return returnModel;
                }
                // add product to cart and updates stock
                else
                {
                    cartItemDb.Quantity = cartItemDb.Quantity + cartItem.Quantity;
                    product.Stock -= cartItem.Quantity;

                }

            }

            _repository.SaveChanges();

            #region ReturnModel fill
            returnModel.Message.status = "success";
            returnModel.Message.text = $"Remaining stock for product is {product.Stock}.";
            returnModel.CartId = cartItem.CartId;
            returnModel.ProductId = cartItemDb.ProductId;
            returnModel.ProductName = cartItemDb.Product.ProductName;
            returnModel.Quantity = cartItemDb.Quantity;
            returnModel.CategoryId = cartItem.CategoryId;
            returnModel.Price = cartItem.Price;
            returnModel.VariantId = cartItem.VariantId;
            #endregion

            return returnModel;
        }
        private async Task<Cart> GetCartFrDb(long cartId)
        {

            return await _repository.Query().Where(x => x.CartId == cartId).Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync();

            /*Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync();*/

        }
        public async Task<ShoppingCartViewModel> GetCart(long cartId)
        {
            // Model to be returned
            var returnList = new ShoppingCartViewModel
            {
                Items = new List<CartProductViewModel>(),
                message = new Result()
            };

            // Retrieve cart from Db
            var cart = await GetCartFrDb(cartId);

            // If cart not exists error message
            if (cart == null)
            {
                returnList.message.status = "error";
                returnList.message.text = "Cart does not exists";
                return returnList;
            }

            // If cart is empty
            if (cart.Items.Count() == 0)
            {
                returnList.message.status = "error";
                returnList.message.text = "Cart is empty";
                return returnList;
            }

            // If exists and not empty -> get products
            foreach (var item in cart.Items)
            {
                item.Product = await _productRepository.GetProductAsync(item.ProductId);
                var prd = CartProductViewModel.GetCartProductViewModel(item);
                returnList.Items.Add(prd);
            }
 
            returnList.CartId = cart.CartId;
            returnList.message.status = "success";
            returnList.message.text = "Shopping cart retrieved";

            return returnList;
        }
    }
}
