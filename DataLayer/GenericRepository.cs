using DataLayer.Interfaces;
using DatLayer.Interfaces;
using DbEntities.Interfaces;
using EmployeeSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DatLayer
{
    public class GenericRepository<T> : IRepository<T> where T : class, IBase, new()
    {
        private readonly EmployeeSystemContext context;

        private readonly DbSet<T> set;

        public int UserId { get; set; }

        public GenericRepository(EmployeeSystemContext context, IUserResolver userService)
        {
            this.context = context;
            set = context.Set<T>();
            UserId = userService.GetUserId();
        }

        public void Add(T entity)
        {
            set.Add(entity);
        }

        public IQueryable<T> All()
        {
            return set.AsQueryable();
        }

        public void Delete(T entity)
        {
            ChangeState(entity, EntityState.Deleted);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ChangeState(entity, EntityState.Deleted);
            }
        }

        public T Find(object id)
        {
            return set.Find(id);
        }

        public T FindOrCreate(int id)
        {
            if (id != 0)
            {
                return Find(id);
            }
            else
            {
                return new T();
            }
        }

        public int Save(T entity)
        {
            if (entity.Id != 0)
            {
                ChangeState(entity, EntityState.Modified);
            }
            else
            {
                set.Add(entity);
            }

            context.SaveChanges();

            //TODO: Check if needed to return id
            return entity.Id;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(T entity)
        {
            ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
