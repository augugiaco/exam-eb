using Exam.Domain.Entities;
using Exam.Domain.Repository;
using System;

namespace Exam.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T,TKey> GetRepository<T,TKey>() where T : EntityBase;
        void Save();
        new void Dispose();
    }
}
