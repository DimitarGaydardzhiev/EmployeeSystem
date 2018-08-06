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
        private readonly UserManager<AspUser> userManager;

        public EmployeeService(IRepository<EmployeeUser> repository, UserManager<AspUser> userManager)
            : base(repository)
        {
            this.userManager = userManager;
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            var result = repository.All().Select(e => new EmployeeViewModel()
            {
                Role = ""
            });

            return result;
        }
    }
}
