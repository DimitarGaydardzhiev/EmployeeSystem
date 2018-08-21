using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;
using System;
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
            var result = GetEmployees(isActive: true);
            return result;
        }

        public IEnumerable<EmployeeViewModel> GetFormerEmployees()
        {
            var result = GetEmployees(isActive: false);
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
                 .Where(e => e.IsActive)
                 .Select(m => new BaseViewModel()
                 {
                     Id = m.Id,
                     Name = $"{m.FirstName} {m.LastName}"
                 });

            return result;
        }

        private IEnumerable<EmployeeViewModel> GetEmployees(bool isActive)
        {
            var result = repository.All()
                .Include(e => e.Department)
                .Where(e => e.IsActive == isActive)
                .Select(e => new EmployeeViewModel()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.Birthday,
                    Description = e.PersonalDescription,
                    StartingDate = e.InCompanyFrom,
                    InCompanyTo = e.InCompanyTo,
                    Department = e.Department.Name,
                    Position = e.EmployeePosition.Name,
                    IsActive = e.IsActive
                });

            return result;
        }
    }
}
