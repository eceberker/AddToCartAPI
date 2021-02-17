using System.Linq;
using System.Threading.Tasks;

namespace AddToCartAPI.Model.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        void Add(T entity);
        void SaveChanges();
        Task SaveAsync();
        void Attach(T entity);

    }
}
