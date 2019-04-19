using Exam.Domain.Entities;

namespace Exam.Application.Interfaces
{
    public interface IService<T,TKey> where T : EntityBase
    {
        //void Delete(object id);
        //void Update(T entity);
    }
}
