using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MainDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MainDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking().ToList();
            }
            else
            {
                return query.AsNoTracking().ToList();
            }
        }

   
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);

            SaveChanges();
        }




        public void Insert(T entity, bool autoSave = true)
        {
            _dbSet.Add(entity);
            if (autoSave) SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        public virtual void DetachLocal(Func<T, bool> predicate)
        {
            var local = _context.Set<T>().Local
                .Where(predicate)
                .FirstOrDefault();

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }

        //private void DetachLocal(Func<T,bool> predicate)
        //{
        //    var local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();
        //    if(local != null)
        //        _context.Entry(local).State = EntityState.Detached;
        //}


        public void Update(T entity, bool autoSave = true)
        {

            _context.Set<T>().Update(entity);

            SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
    }
}

