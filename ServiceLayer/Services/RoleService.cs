using DTOs.ViewModels;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IEnumerable<RoleViewModel> All()
        {
            var result = roleManager.Roles.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name
            })
            .OrderByDescending(r => r.Id);

            return result;
        }
    }
}
