using NICS.System.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;
using System.Linq.Expressions;

using System.Text;
using System.Threading.Tasks;

namespace NICS.System.Data.Implementaion
{
    public abstract class DataRepository<T, TC> : IDataRepository<T> where T : class where TC : DbContext, new()
    {

        private TC _entities = new TC();
        public TC Context
        {

            get { return _entities; }
            set { _entities = value; }
        }


        public virtual T Add(T entity)
        {
            var adding = _entities.Set<T>().Add(entity);
            return adding;
        }


        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual IEnumerable<T> Get()
        {
            IEnumerable<T> query = _entities.Set<T>();
            return query;
        }

        public virtual T Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public virtual void Remove(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public virtual void Update(T entity)
        {

            var query = _entities.Set<T>().Attach(entity);
            //dbSet.Attach(entityToUpdate);
            _entities.Entry(entity).State = EntityState.Modified;

        }

        public virtual List<T> GetallRecords()
        {
            var query = from r in _entities.Set<T>()
                        select r;
            return query.ToList();
        }
    }
}
