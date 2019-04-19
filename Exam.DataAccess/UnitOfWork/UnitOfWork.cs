using Exam.Domain.Entities;
using Exam.Domain.Interfaces.UnitOfWork;
using Exam.Domain.Repository;
using Exam.Infraestructure.Repository;
using System;

namespace Exam.Infraestructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamContext _context;
        private bool disposed = false;

        public UnitOfWork(ExamContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T,TKey> GetRepository<T,TKey>() where T : EntityBase
        {
            return new Repository<T,TKey>(_context);
        }
    }
}
