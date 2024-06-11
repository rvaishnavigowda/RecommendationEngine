using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Repository.GenericRepo
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> GetByStringId(string id);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<int> Create(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int id);

        Task<int> Add(T entity);

    }
}
