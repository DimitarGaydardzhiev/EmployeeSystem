﻿using AutoMapper;
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
        private readonly IMapper mapper;

        public RequestService(
            IRepository<Request> repository,
            IRequestTypeService requestTypeService,
            IMapper mapper)
            : base(repository)
        {
            this.requestTypeService = requestTypeService;
            this.mapper = mapper;
        }

        public IEnumerable<BaseViewModel> GetRequestTypes()
        {
            var result = requestTypeService.All();
            return result;
        }

        public void Save(RequestViewModel model)
        {
            var userId = repository.UserId;

            var request = repository
                .All()
                .FirstOrDefault(r => r.EmployeeUserId == userId &&
                                    (r.From >= model.From && model.From <= model.To));

            if (request != null)
                throw new Exception(ErrorMessages.ThereIsAlreadyRequestForTheseDatesMessage);

            var result = repository.FindOrCreate(model.Id);

            if (!CanEdit(model))
                throw new Exception(ErrorMessages.CanNotEditAnotherUserRequest);

            result.EmployeeUserId = userId;
            result.From = model.From;
            result.To = model.To;
            result.RequestTypeId = model.RequestTypeId;
            result.Description = model.Description;
            result.IsApproved = false;

            repository.Save(result);
        }

        public IEnumerable<RequestViewModel> GetMyRequests()
        {
            var requests = repository.All()
                .Include(r => r.EmployeeUser)
                .Include(r => r.RequestType)
                .Where(r => r.EmployeeUserId == repository.UserId)
                .ToList();

            return mapper.Map<List<Request>, List<RequestViewModel>>(requests);
        }

        public IEnumerable<RequestViewModel> GetPendingRequests()
        {
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
            var requests = repository.All()
                .Include(r => r.EmployeeUser)
                .Include(r => r.RequestType)
                .Where(r => r.IsApproved == isApproved)
                .ToList();

            return mapper.Map<List<Request>, List<RequestViewModel>>(requests);
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

        public bool CanEdit(RequestViewModel model)
        {
            var userId = repository.UserId;
            var request = repository.Find(model.Id);

            if (request != null && request.EmployeeUserId != userId)
                return false;

            return true;
        }
    }
}
