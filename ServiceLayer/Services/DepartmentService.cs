using System;
using System.Collections.Generic;
using System.Linq;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ErrorUtils;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        private readonly IRepository<EmployeeUser> employeeRepository;

        public DepartmentService(
            IRepository<Department> repository,
            IRepository<EmployeeUser> employeeRepository)
            : base(repository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<DepartmentViewModel> All()
        {
            var result = repository.All()
                .Select(d => new DepartmentViewModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                    EmployeesCount = d.Employees.Count()
                });

            return result;
        }

        public void Add(DepartmentViewModel model)
        {
            var department = repository.All()
                .FirstOrDefault(d => d.Name == model.Name);

            if (department != null)
                throw new Exception(ErrorMessages.ObjectAlreadyAddedMessage);

            var result = new Department()
            {
                Name = model.Name
            };

            repository.Add(result);
            repository.SaveChanges();
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
