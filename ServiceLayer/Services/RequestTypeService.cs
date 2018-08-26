using AutoMapper;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.Models;
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

        public IEnumerable<BaseDto> All()
        {
            var result = repository.All()
                 .Select(rt => new BaseDto()
                 {
                     Id = rt.Id,
                     Name = rt.Name
                 });

            return result;
        }
    }
}
