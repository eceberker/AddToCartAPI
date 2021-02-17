using AddToCartAPI.Model.Models;
using AddToCartAPI.Model.Repository;
using AddToCartAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AddToCartAPI.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetProductAsync(long productId)
        {
            return await _repository.Query().Where(x => x.ProductId == productId).FirstOrDefaultAsync();
        }

    }
}
