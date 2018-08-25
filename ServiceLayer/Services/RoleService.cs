using AutoMapper;
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
        private readonly IMapper mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public IEnumerable<RoleViewModel> All()
        {
            //var result = roleManager.Roles.Select(r => new RoleViewModel()
            //{
            //    Id = r.Id,
            //    Name = r.Name
            //})
            //.OrderByDescending(r => r.Id);

            var roles = roleManager.Roles
                .OrderByDescending(r => r.Id)
                .ToList();

            return mapper.Map<List<IdentityRole>, List<RoleViewModel>>(roles);
        }
    }
}
