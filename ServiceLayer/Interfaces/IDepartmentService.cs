using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentViewModel> All();

        void Save(DepartmentViewModel model);

        void Delete(int departmentId);
    }
}
