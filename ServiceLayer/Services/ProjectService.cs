using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.ViewModels;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Services
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        public ProjectService(IRepository<Project> repository)
            : base(repository)
        {
        }

        public IEnumerable<ProjectViewModel> GetCompanyProjects()
        {
            var result = repository.All()
                 .Include(p => p.EmployeeUserProjects)
                 .ThenInclude(p => p.EmployeeUser)
                 .Select(p => new ProjectViewModel()
                 {
                     Employees = p.EmployeeUserProjects.Select(eup => new BaseViewModel()
                     {
                         Name = $"{eup.EmployeeUser.FirstName} {eup.EmployeeUser.LastName}"
                     }),
                     Name = p.Name,
                     StartDate = p.StartDate,
                     EndDate = p.EndDate,
                     Status = p.Status.ToString()
                 });

            return result;
        }
    }
}
