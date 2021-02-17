using AddToCartAPI.Model.Models;
using System.Threading.Tasks;

namespace AddToCartAPI.Service.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(long productId);

    }
}
