using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class RequestService : BaseService<Request>, IRequestService
    {
        private readonly IRequestTypeService requestTypeService;

        public RequestService(
            IRepository<Request> repository,
            IRequestTypeService requestTypeService)
            : base(repository)
        {
            this.requestTypeService = requestTypeService;
        }

        public IEnumerable<BaseViewModel> GetRequestTypes()
        {
            var result = requestTypeService.All();
            return result;
        }

        public void Add(RequestViewModel model)
        {
            var userId = repository.UserId;

            var request = repository
                .All()
                .FirstOrDefault(r => r.EmployeeUserId == userId &&
                                    (r.From <= model.From && model.From <= model.To));

            // TODO: Exception handling
            if (request != null)
            {
                throw new System.Exception("There is already a request for these dates");
            }

            var newRequest = new Request()
            {
                EmployeeUserId = userId,
                From = model.From,
                To = model.To,
                RequestTypeId = model.RequestTypeId,
                Description = model.Description,
                IsApproved = model.IsApproved
            };

            repository.Add(newRequest);
            repository.SaveChanges();
        }

        public IEnumerable<RequestViewModel> GetMyRequests()
        {
            var result = repository
                .All()
                .Where(r => r.EmployeeUserId == repository.UserId)
                .Include(r => r.RequestType)
                .Select(r => new RequestViewModel()
                {
                    Description = r.Description,
                    From = r.From,
                    To = r.To,
                    RequestType = r.RequestType.Name
                });

            return result;
        }
    }
}
