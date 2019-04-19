using Autofac;
using Exam.Application.Exceptions;
using Exam.Application.Interfaces;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces.UnitOfWork;
using Exam.Domain.Services;
using System.Threading.Tasks;

namespace Exam.Application.Services
{
    public abstract class ServiceBase<T,TKey> : IService<T,TKey> where T : EntityBase
    {
        protected readonly IDomainLogic<T,TKey> _domainService;
        protected readonly IUnitOfWork _uow;

        public ServiceBase(IDomainLogic<T,TKey> domainService,IUnitOfWork uow)
        {
            _domainService = domainService;
            _uow = uow;        
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            var entity = await _domainService.GetByIdAsync(id);

            if (entity == null)
                throw new EntityNotFoundException();

            return entity;
        }

        
    }
}
