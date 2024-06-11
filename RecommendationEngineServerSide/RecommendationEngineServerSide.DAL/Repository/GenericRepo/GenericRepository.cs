using Microsoft.EntityFrameworkCore;
using RecommendationEngineServerSide.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBContext _context;
        protected readonly DbSet<T> _table;

        public GenericRepository(DBContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            return entity;
        }

        public async Task<T> GetByStringId(string id)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            return entity;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _table.Where(predicate).ToListAsync();
        }

        public async Task<int> Create(T entity)
        {
            await _table.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
                return 0;

            _table.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _table.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
