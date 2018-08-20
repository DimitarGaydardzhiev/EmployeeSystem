using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Services
{
    public class EmployeeService : BaseService<EmployeeUser>, IEmployeeService
    {
        private readonly IRoleService roleService;
        private readonly IPositionService positionService;
        private readonly IDepartmentService departmentService;
        private readonly IAccountService accountService;
        private readonly UserManager<AspUser> userManager;

        public EmployeeService(
            IRepository<EmployeeUser> repository,
            IRoleService roleService,
            IPositionService positionService,
            IDepartmentService departmentService,
            IAccountService accountService,
            UserManager<AspUser> userManager)
            : base(repository)
        {
            this.roleService = roleService;
            this.positionService = positionService;
            this.departmentService = departmentService;
            this.accountService = accountService;
            this.userManager = userManager;
        }

        public IEnumerable<EmployeeViewModel> All()
        {
            // TODO: Get role name
            var result = repository.All()
                .Include(e => e.Department)
                .Select(e => new EmployeeViewModel()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.Birthday,
                    Description = e.PersonalDescription,
                    StartingDate = e.InCompanyFrom,
                    Department = e.Department.Name,
                    Position = e.EmployeePosition.Name,
                    //Role = userManager.GetRolesAsync(e.AspUser).Result[0]
                });

            return result;
        }

        public IEnumerable<RoleViewModel> GetRoles()
        {
            var result = roleService.All();
            return result;
        }

        public IEnumerable<BaseViewModel> GetPositions()
        {
            var positions = positionService.All();
            return positions;
        }

        public IEnumerable<BaseViewModel> GetDepartments()
        {
            var departments = departmentService.All();
            return departments;
        }

        public IEnumerable<BaseViewModel> GetManagers()
        {
            var result = repository.All()
                 .Select(m => new BaseViewModel()
                 {
                     Id = m.Id,
                     Name = $"{m.FirstName} {m.LastName}"
                 });

            return result;
        }
    }
}
