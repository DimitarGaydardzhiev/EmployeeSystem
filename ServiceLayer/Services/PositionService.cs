﻿using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.InputModels;
using DTOs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Services
{
    public class PositionService : BaseService<EmployeePosition>, IPositionService
    {
        private readonly IRepository<EmployeeUser> employeeRepository;

        public PositionService(
            IRepository<EmployeePosition> repository,
            IRepository<EmployeeUser> employeeRepository)
            : base(repository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<PositionViewModel> All()
        {
            var result = repository.All()
                .Select(p => new PositionViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    EmployeesCount = employeeRepository.All().Where(e => e.EmployeePositionId == p.Id).Count()
                });

            return result;
        }

        public void Add(PositionInputModel model)
        {
            var position = repository.All().FirstOrDefault(p => p.Name == model.Name);
            if (position != null)
            {
                throw new Exception("Position already added");
            }

            position = new EmployeePosition()
            {
                Name = model.Name
            };

            repository.Save(position);
        }
    }
}