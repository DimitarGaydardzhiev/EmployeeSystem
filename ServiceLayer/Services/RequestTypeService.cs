using AutoMapper;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using ServiceLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Services
{
    public class RequestTypeService : BaseService<RequestType>, IRequestTypeService
    {
        public RequestTypeService(IRepository<RequestType> repository)
            : base(repository)
        {
        }

        public IEnumerable<BaseViewModel> All()
        {
            var result = repository.All()
                 .Select(rt => new BaseViewModel()
                 {
                     Id = rt.Id,
                     Name = rt.Name
                 });

            return result;
        }
    }
}
