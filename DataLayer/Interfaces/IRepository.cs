using System.Collections.Generic;
using System.Linq;

namespace DatLayer.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> All();

        T Find(object id);

        T FindOrCreate(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        int SaveChanges();

        int Save(T entity);
    }
}
