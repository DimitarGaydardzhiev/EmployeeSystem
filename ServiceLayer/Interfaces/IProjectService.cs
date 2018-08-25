using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectViewModel> GetCompanyProjects();

        ProjectViewModel GetEmployees();

        void Save(ProjectViewModel model);

        IEnumerable<ProjectViewModel> GetUserProjects();

        void Delete(int id);
    }
}
