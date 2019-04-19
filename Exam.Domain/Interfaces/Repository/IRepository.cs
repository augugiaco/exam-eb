using System.Linq;
using System.Threading.Tasks;

namespace Exam.Domain.Repository
{
    public interface IRepository<T,TKey>
    {
        IQueryable<T> GetAll(bool isTracked = false);
        Task<T> GetByIdAsync(TKey entiyId);
        void Insert(T entity);
        Task DeleteAsync(TKey entityId);
        void Update(T entity);
    }
}
