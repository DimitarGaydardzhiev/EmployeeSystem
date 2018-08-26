using System;
using System.Linq;
using System.Threading.Tasks;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.Models;
using EmployeeSystem.Models;
using EmployeeSystem.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ErrorUtils;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AspUser> userManager;
        private readonly SignInManager<AspUser> signInManager;
        private readonly IRepository<EmployeeUser> employeeUserRepository;

        public AccountService(
            UserManager<AspUser> userManager,
            SignInManager<AspUser> signInManager,
            IRepository<EmployeeUser> employeeUserRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.employeeUserRepository = employeeUserRepository;
        }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
            var user = employeeUserRepository.All()
                .Include(u => u.AspUser)
                .FirstOrDefault(u => u.AspUser.Email == model.Email);

            if (user != null && !user.IsActive)
                return SignInResult.Failed;

            return await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

        public async Task<IdentityResult> Register(EmployeeDto model)
        {
            var user = new AspUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //TODO: Add toastr
                await userManager.AddToRoleAsync(user, "user");

                user.EmployeeUser = new EmployeeUser()
                {
                    IsActive = true,
                    Birthday = model.DateOfBirth,
                    DepartmentId = model.DepartmentId,
                    EmployeePositionId = model.PositionId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    InCompanyFrom = model.StartingDate,
                    PersonalDescription = model.Description,
                    ManagerId = model.ManagerId,
                };

                await userManager.UpdateAsync(user);
            }

            return result;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }
    }
}
