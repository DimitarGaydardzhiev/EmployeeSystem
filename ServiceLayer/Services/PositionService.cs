using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using ServiceLayer.Interfaces;
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
    }
}
