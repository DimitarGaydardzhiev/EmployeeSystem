using System.Threading.Tasks;
using DbEntities.Models;
using DTOs.ViewModels;
using EmployeeSystem.Models;
using EmployeeSystem.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AspUser> userManager;
        private readonly SignInManager<AspUser> signInManager;

        public AccountService(
            UserManager<AspUser> userManager,
            SignInManager<AspUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
            return await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

        public async Task<IdentityResult> Register(EmployeeViewModel model)
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
