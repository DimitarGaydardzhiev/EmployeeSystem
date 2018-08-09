using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeViewModel> GetAll();

        IEnumerable<RoleViewModel> GetRoles();

        IEnumerable<BaseViewModel> GetPositions();
    }
}
