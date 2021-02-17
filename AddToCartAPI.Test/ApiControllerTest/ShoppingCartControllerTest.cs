using AddToCartAPI.Service.Interfaces;
using AddToCartAPI.Service.ViewModels;
using AddToCartAPI.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AddToCartAPI.Test.ApiControllerTest
{
    public class ShoppingCartControllerTest
    {
        private ShoppingCartController _shoppingCartController;
        private Mock<ICartService> _cartServiceMock = new Mock<ICartService>();

        public ShoppingCartControllerTest()
        {
            _shoppingCartController = new ShoppingCartController(_cartServiceMock.Object);

        }

        [Fact]
        public async Task Get_ShoppingCart_ByCartId_Not_Found_Test()
        {
            // Arrange     
            var cartMock = new ShoppingCartViewModel()
            {
                CartId = 1
            };

            _cartServiceMock.Setup(m => m.GetCart(cartMock.CartId)).Returns(Task.FromResult(new ShoppingCartViewModel()));

            // Act
            var result = await _shoppingCartController.GetShoppingCart(cartMock.CartId);


            // Assert

            var notFound = result as NotFoundObjectResult;
            Assert.NotNull(notFound);



        }
        [Fact]
        public async Task Add_Product_To_Shopping_Cart_Should_Not_Be_Null()
        {
            // Arrange            
            var product = new CartProductViewModel()
            {
                CartId = 1,
                ProductId = 1,
            };
            _cartServiceMock.Setup(x => x.AddProductToCart(product)).Returns(Task.FromResult(product));

            // Act
            var result = await _shoppingCartController.AddProductToShoppingCart(product);

            // Assert
            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as CartProductViewModel;
            Assert.NotNull(content);
        }
    }
}
