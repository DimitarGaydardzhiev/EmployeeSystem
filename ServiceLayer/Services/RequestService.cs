using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ErrorUtils;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
                                    (r.From >= model.From && model.From <= model.To));

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
                IsApproved = false
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
                    Id = r.Id,
                    Description = r.Description,
                    From = r.From,
                    To = r.To,
                    RequestType = r.RequestType.Name,
                    IsApproved = r.IsApproved
                });

            return result;
        }

        public IEnumerable<RequestViewModel> GetPendingRequests()
        {
            // TODO: Get requests for employees by mnanager
            var result = GetRequests(false);
            return result;
        }

        public IEnumerable<RequestViewModel> GetApprovedRequests()
        {
            var result = GetRequests(true);
            return result;
        }

        public void ApproveRequest(int requestId)
        {
            ManageRequest(requestId, true);
        }

        public void UnapproveRequest(int requestId)
        {
            ManageRequest(requestId, false);
        }

        private IEnumerable<RequestViewModel> GetRequests(bool isApproved)
        {
            var result = repository.All()
                .Include(r => r.EmployeeUser)
                .Include(r => r.RequestType)
                .Where(r => r.IsApproved == isApproved)
                .Select(r => new RequestViewModel()
                {
                    Id = r.Id,
                    User = $"{r.EmployeeUser.FirstName} {r.EmployeeUser.LastName}",
                    From = r.From,
                    To = r.To,
                    RequestType = r.RequestType.Name,
                    Description = r.Description,
                    IsApproved = r.IsApproved
                });

            return result;
        }

        private void ManageRequest(int requestId, bool status)
        {
            var request = repository.Find(requestId);

            if (request == null)
            {
                throw new Exception("Request not found");
            }

            request.IsApproved = status;
            repository.Update(request);
            repository.SaveChanges();
        }

        public override void Delete(int id)
        {
            var request = repository.All()
                .FirstOrDefault(d => d.Id == id);

            if (request != null && request.IsApproved)
                throw new InvalidDeleteException(ErrorMessages.ConNotDeleteApprovedRequestMessage);

            base.Delete(id);
        }
    }
}
