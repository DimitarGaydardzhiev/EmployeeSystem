using System.Collections.Generic;
using System.Linq;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class EmployeeService : BaseService<EmployeeUser>, IEmployeeService
    {
        private readonly IRoleService roleService;
        private readonly IPositionService positionService;

        public EmployeeService(
            IRepository<EmployeeUser> repository,
            IRoleService roleService,
            IPositionService positionService)
            : base(repository)
        {
            this.roleService = roleService;
            this.positionService = positionService;
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return null;
        }

        public IEnumerable<RoleViewModel> GetRoles()
        {
            var result = roleService.All();
            return result;
        }

        public IEnumerable<BaseViewModel> GetPositions()
        {
            var result = positionService.All();
            return result;
        }
    }
}
