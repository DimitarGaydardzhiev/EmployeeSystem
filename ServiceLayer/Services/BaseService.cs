using DatLayer.Interfaces;
using DbEntities.Models;
using System;

namespace ServiceLayer.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly IRepository<T> repository;

        public BaseService(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual void Delete(int id)
        {
            var entity = repository.Find(id);

            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            if (typeof(T) == typeof(EmployeeUser))
            {
                var user = entity as EmployeeUser;
                user.IsActive = false;
                user.InCompanyTo = DateTime.Now;
            }
            else
            {
                repository.Delete(entity);
            }

            repository.SaveChanges();
        }
    }
}
