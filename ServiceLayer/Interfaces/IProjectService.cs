using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectViewModel> GetCompanyProjects();

        ProjectViewModel GetEmployees();

        void Add(ProjectViewModel model);

        IEnumerable<ProjectViewModel> GetUserProjects();
    }
}
