using DatLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly IRepository<T> repository;

        public BaseService(IRepository<T> repository)
        {
            this.repository = repository;
        }
    }
}
