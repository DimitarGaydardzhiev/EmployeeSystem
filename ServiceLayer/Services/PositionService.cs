using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using ServiceLayer.Interfaces;
using System.Collections.Generic;

namespace ServiceLayer.Services
{
    public class PositionService : BaseService<EmployeePosition>, IPositionService
    {
        public PositionService(IRepository<EmployeePosition> repository)
            : base(repository)
        {
        }

        public IEnumerable<PositionViewModel> GetAll()
        {
            return null;
        }
    }
}
