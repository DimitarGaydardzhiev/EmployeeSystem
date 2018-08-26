using DTOs.Models;
using EmployeeSystem.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> Login(LoginViewModel model);

        Task<IdentityResult> Register(EmployeeDto model);

        Task Logout();
    }
}
