using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeViewModel> All();

        IEnumerable<RoleViewModel> GetRoles();

        IEnumerable<BaseViewModel> GetPositions();

        IEnumerable<BaseViewModel> GetDepartments();

        IEnumerable<BaseViewModel> GetManagers();
    }
}
