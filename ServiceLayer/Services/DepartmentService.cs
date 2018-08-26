using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ErrorUtils;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        private readonly IMapper mapper;
        private readonly IRepository<EmployeeUser> employeeRepository;

        public DepartmentService(
            IRepository<Department> repository,
            IRepository<EmployeeUser> employeeRepository,
            IMapper mapper)
            : base(repository)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public IEnumerable<DepartmentDto> All()
        {
            var departments = repository.All().Include(d => d.Employees).ToList();

            var result = mapper.Map<List<Department>, List<DepartmentDto>>(departments);

            return result;
        }

        public void Save(DepartmentDto model)
        {
            var department = repository.All()
                .FirstOrDefault(d => d.Name == model.Name);

            if (department != null)
                throw new Exception(ErrorMessages.ObjectAlreadyAddedMessage);

            var result = repository.FindOrCreate(model.Id);
            result.Name = model.Name;

            repository.Save(result);
        }

        public override void Delete(int id)
        {
            var department = repository.All()
                .Include(d => d.Employees)
                .FirstOrDefault(d => d.Id == id);

            if (department != null && department.Employees.Count() > 0)
                throw new InvalidDeleteException(ErrorMessages.HasEmployeesMessage);

            base.Delete(id);
        }
    }
}
