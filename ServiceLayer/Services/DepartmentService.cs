using System;
using System.Collections.Generic;
using System.Linq;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
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

        public IEnumerable<DepartmentViewModel> GetAll()
        {
            var result = repository.All()
                .Select(d => new DepartmentViewModel()
                {
                    Name = d.Name,
                    EmployeesCount = employeeRepository.All().Where(e => e.DepartmentId == d.Id).Count()
                });

            return result;
        }

        public void Add(DepartmentViewModel model)
        {
            var department = repository.All()
                .FirstOrDefault(d => d.Name == model.Name);

            if (department != null)
            {
                throw new Exception("Department already added");
            }

            var result = new Department()
            {
                Name = model.Name
            };

            repository.Add(result);
            repository.SaveChanges();
        }
    }
}
