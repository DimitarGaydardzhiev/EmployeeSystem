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
        private readonly IEmployeeService employeeService;
        private readonly IRepository<EmployeeUserProject> employeeUserProjectRepository;

        public ProjectService(
            IRepository<Project> repository,
            IEmployeeService employeeService,
            IRepository<EmployeeUserProject> employeeUserProjectRepository)
            : base(repository)
        {
            this.employeeService = employeeService;
            this.employeeUserProjectRepository = employeeUserProjectRepository;
        }

        public IEnumerable<ProjectViewModel> GetCompanyProjects()
        {
            var result = repository.All()
                 .Include(p => p.EmployeeUserProjects)
                 .ThenInclude(p => p.EmployeeUser)
                 .Select(p => new ProjectViewModel()
                 {
                     Employees = p.EmployeeUserProjects.Select(eup => new MultiSelectViewModel()
                     {
                         Id = eup.EmployeeUserId,
                         Name = $"{eup.EmployeeUser.FirstName} {eup.EmployeeUser.LastName}"
                     }).ToList(),
                     Name = p.Name,
                     StartDate = p.StartDate,
                     EndDate = p.EndDate,
                     Status = p.Status.ToString()
                 });

            return result;
        }

        public ProjectViewModel GetEmployees()
        {
            var result = new ProjectViewModel()
            {
                Employees = employeeService.All().Select(r => new MultiSelectViewModel()
                {
                    Id = r.Id,
                    Name = $"{r.FirstName} {r.LastName}"
                }).ToList()
            };

            return result;
        }

        public void Add(ProjectViewModel model)
        {
            var project = repository.All()
                .FirstOrDefault(p => p.Name == model.Name);

            if (project != null)
            {
                throw new Exception("Project already exists");
            }

            var result = new Project()
            {
                Name = model.Name,
                ProjectStatusId = (int)DTOs.ProjectStatus.NotStarted,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            var projectId = repository.Save(result);
            
            model.Employees.ForEach(e =>
            {
                var employeeUserProject = new EmployeeUserProject()
                {
                    EmployeeUserId = e.Id,
                    ProjectId = projectId
                };

                employeeUserProjectRepository.Add(employeeUserProject);
            });

            employeeUserProjectRepository.SaveChanges();
            repository.SaveChanges();
        }
    }
}
