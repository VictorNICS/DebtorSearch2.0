using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NICS.System.Data.Interface
{
    public interface IDataRepository
    {

    }

    public interface IDataRepository<T> : IDataRepository where T : class
    {
        T Add(T entity);
        void Remove(T entity);
        void Remove(int Id);
        void Update(T entity);
        IEnumerable<T> Get();
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Save();

    }
}
