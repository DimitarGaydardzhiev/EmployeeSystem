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
                    EmployeesCount = employeeRepository.All().Where(e => e.EmployeePositionId == p.Id && e.IsActive).Count()
                });

            return result;
        }

        public void Save(PositionViewModel model)
        {
            var position = repository.All().FirstOrDefault(p => p.Name == model.Name);

            if (position != null)
                throw new Exception(ErrorMessages.ObjectAlreadyAddedMessage);

            var result = repository.FindOrCreate(model.Id);

            if (result.Employees.Count() > 0)
                throw new Exception(ErrorMessages.UnableToEditPositionWithEmployeesMessage);

            result.Name = model.Name;

            repository.Save(result);
        }

        public override void Delete(int id)
        {
            var position = repository.All()
                .Include(p => p.Employees)
                .FirstOrDefault(d => d.Id == id);

            if (position != null && position.Employees.Count() > 0)
                throw new InvalidDeleteException(ErrorMessages.HasEmployeesMessage);

            base.Delete(id);
        }
    }
}
