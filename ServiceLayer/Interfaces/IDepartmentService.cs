using DTOs.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> All();

        void Save(DepartmentDto model);

        void Delete(int departmentId);
    }
}
