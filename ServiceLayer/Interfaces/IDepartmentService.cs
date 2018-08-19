using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentViewModel> GetAll();

        void Add(DepartmentViewModel model);
    }
}
