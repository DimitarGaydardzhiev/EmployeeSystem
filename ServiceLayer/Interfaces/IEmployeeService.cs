using DTOs.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> All();

        IEnumerable<RoleDto> GetRoles();

        IEnumerable<BaseDto> GetPositions();

        IEnumerable<BaseDto> GetDepartments();

        IEnumerable<BaseDto> GetManagers();

        IEnumerable<EmployeeDto> GetFormerEmployees();

        void Delete(int employeeId);
    }
}
