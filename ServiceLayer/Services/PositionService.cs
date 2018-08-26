using AutoMapper;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.Models;
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
        private readonly IMapper mapper;

        public PositionService(
            IRepository<EmployeePosition> repository,
            IRepository<EmployeeUser> employeeRepository,
            IMapper mapper)
            : base(repository)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public IEnumerable<PositionDto> All()
        {
            var positions = repository.All().Include(d => d.Employees).ToList();

            var result = mapper.Map<List<EmployeePosition>, List<PositionDto>>(positions);

            return result;
        }

        public void Save(PositionDto model)
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
