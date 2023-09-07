using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DataContext _context = null;
        private DbSet<T> table = null;

        public GenericRepository(DataContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public async Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> query)
        {
            return table.Where(query);
        }
        public async Task<T> GetById(int id)
        {
            return table.Find(id);
        }
        public async Task Insert(T obj)
        {
            await table.AddAsync(obj);
        }
        public async Task Update(T obj2)
        {
            table.Attach(obj2);
            _context.Entry(obj2).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public async Task<object> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<T> UserGetById(string id)
        {
            return table.Find(id);
        }
    }
}
