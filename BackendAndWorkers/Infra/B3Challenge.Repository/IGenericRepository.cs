using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = null);
        void Insert(T entity, bool autoSave = true);
        void Update(T entity, bool autoSave = true);
        void Delete(T entity);
        void SaveChanges();
        IQueryable<T> GetAll();
    }
}
