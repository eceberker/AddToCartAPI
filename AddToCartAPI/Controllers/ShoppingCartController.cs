using AddToCartAPI.Service.Interfaces;
using AddToCartAPI.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AddToCartAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ICartService _cartSerivce;
        public ShoppingCartController(ICartService cartService)
        {
            _cartSerivce = cartService;
        }


        /// <summary>
        /// Gets Shopping Cart with cartId
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        /// <response code ="200">Returns requested Shopping Cart</response>
        /// <response code ="404">Returns when Shopping Cart is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{cartId}")]
        public async Task<IActionResult> GetShoppingCart([FromRoute] long cartId)
        {

            ShoppingCartViewModel cartItems = await _cartSerivce.GetCart(cartId);

            if (cartItems.Items.Count() == 0)
            {
                return NotFound(cartItems);
            }
            else
            {
                return Ok(cartItems);
            }


        }
        /// <summary>
        /// Adds product to Shopping Cart requested from body
        /// </summary>
        /// <param name="cartProduct"></param>
        /// <remarks>
        /// Message will be filled with response
        /// 
        /// Sample Request
        ///         
        ///         
        /// 
        ///         POST /ShoppingCart/AddProductToShoppingCart
        ///         {
        ///             "CartId" : 9,
        ///             "ProductId": 8,
        ///             "ProductName": "test_product6",
        ///             "Quantity" : 1,
        ///             "Price" : 2000,
        ///             "VariantId" : 5,
        ///             "CategoryId": 2       
        ///         }
        /// 
        /// 
        /// </remarks>
        /// <returns></returns>
        /// <response code ="200">Returns added product and Database message, if there is an error return Database message</response>
        /// 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> AddProductToShoppingCart([FromBody] CartProductViewModel cartProduct)
        {

            CartProductViewModel addedItem = await _cartSerivce.AddProductToCart(cartProduct);

            return Ok(addedItem);

        }

    }
}
