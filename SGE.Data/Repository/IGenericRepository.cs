using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> query);
        Task<T> GetById(int id);
        Task<T> UserGetById(string id);
        Task Insert(T obj);
        Task Update(T obj);
        void Delete(object id);
        Task<object> Save();
    }
}
