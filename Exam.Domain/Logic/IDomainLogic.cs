using Exam.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Domain.Services
{
    public interface IDomainLogic<T,TKey> where T : EntityBase
    {
        Task<T> GetByIdAsync(TKey id);
        Task DeleteAsync(TKey id);
        void Update(T entity);
        IQueryable<T> GetAll(bool isTracked = false);
        void Insert(T entity);
    }
}
