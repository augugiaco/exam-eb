using System.Linq;
using System.Threading.Tasks;
using Exam.Domain.Entities;
using Exam.Domain.Repository;
using Exam.Domain.Services;

namespace Exam.Domain.Base.Services
{
    public class DomainBaseLogic<T,TKey> : IDomainLogic<T,TKey> where T : EntityBase
    {
        protected readonly IRepository<T,TKey> _repository;

        public DomainBaseLogic(IRepository<T,TKey> repository)
        {
            _repository = repository;       
        }

        /// <summary>
        /// Retrieve an Entity by Id and throws an NotFoundException if the entity was not found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(TKey id)
        {
            return await _repository.GetByIdAsync(id);        
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public async Task DeleteAsync(TKey id)
        {
            await _repository.DeleteAsync(id);
            
        }

        public virtual void Insert(T entity)
        {
            _repository.Insert(entity);
        }

        public IQueryable<T> GetAll(bool isTracked = false)
        {
            return _repository.GetAll(isTracked);
        }
    }
}
