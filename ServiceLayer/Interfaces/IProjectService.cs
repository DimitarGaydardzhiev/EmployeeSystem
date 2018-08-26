using DTOs.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetCompanyProjects();

        ProjectDto GetEmployees();

        void Save(ProjectDto model);

        IEnumerable<ProjectDto> GetUserProjects();

        void Delete(int id);
    }
}
