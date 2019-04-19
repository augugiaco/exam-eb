using Exam.Domain.Entities;
using Exam.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Infraestructure.Repository
{
    public class Repository<TEntity,TKey> : IRepository<TEntity,TKey> where TEntity : EntityBase
    {
        private readonly ExamContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ExamContext _context)
        {
            this._context = _context;
            this._dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IQueryable<TEntity> GetAll(bool isTracked = false)
        {
            return isTracked ? _dbSet.AsQueryable() : _dbSet.AsQueryable().AsNoTracking();
        }

    }
}
