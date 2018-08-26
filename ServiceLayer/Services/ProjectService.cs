using AutoMapper;
using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ErrorUtils;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Services
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        private readonly IEmployeeService employeeService;
        private readonly IRepository<EmployeeUserProject> employeeUserProjectRepository;
        private readonly IMapper mapper;

        public ProjectService(
            IRepository<Project> repository,
            IEmployeeService employeeService,
            IRepository<EmployeeUserProject> employeeUserProjectRepository,
            IMapper mapper)
            : base(repository)
        {
            this.employeeService = employeeService;
            this.employeeUserProjectRepository = employeeUserProjectRepository;
            this.mapper = mapper;
        }

        public IEnumerable<ProjectDto> GetCompanyProjects()
        {
            var result = repository.All()
                 .Include(p => p.EmployeeUserProjects)
                 .ThenInclude(p => p.EmployeeUser)
                 .Select(p => new ProjectDto()
                 {
                     Employees = p.EmployeeUserProjects
                     .Where(eup => eup.EmployeeUser.IsActive)
                     .Select(eup => new MultiSelectDto()
                     {
                         Id = eup.EmployeeUserId,
                         Name = $"{eup.EmployeeUser.FirstName} {eup.EmployeeUser.LastName}"
                     })
                     .ToList(),
                     Id = p.Id,
                     Name = p.Name,
                     StartDate = p.StartDate,
                     EndDate = p.EndDate,
                     Status = p.Status.ToString()
                 });

            return result;
        }

        public ProjectDto GetEmployees()
        {
            var result = new ProjectDto()
            {
                Employees = employeeService.All().Select(r => new MultiSelectDto()
                {
                    Id = r.Id,
                    Name = $"{r.FirstName} {r.LastName}"
                }).ToList()
            };

            return result;
        }

        public void Save(ProjectDto model)
        {
            var project = repository.All()
                .FirstOrDefault(p => p.Name == model.Name);

            if (project != null && model.Id == 0)
                throw new Exception(ErrorMessages.ObjectAlreadyAddedMessage);

            var result = repository.FindOrCreate(model.Id);
            result.Name = model.Name;
            result.ProjectStatusId = (int)DTOs.ProjectStatus.NotStarted;
            result.StartDate = model.StartDate;
            result.EndDate = model.EndDate;

            var projectId = repository.Save(result);

            var listEup = new List<EmployeeUserProject>();

            var employeeUserProjects = employeeUserProjectRepository
                .All()
                .Where(eup => eup.ProjectId == projectId)
                .ToList();

            model.Employees.Where(e => e.IsSelected)
                .ToList()
                .ForEach(e =>
              {
                  var employeeUserProject = new EmployeeUserProject()
                  {
                      EmployeeUserId = e.Id,
                      ProjectId = projectId
                  };

                  listEup.Add(employeeUserProject);
              });

            employeeUserProjects = listEup;
            result.EmployeeUserProjects = employeeUserProjects;
            repository.Save(result);
        }

        public IEnumerable<ProjectDto> GetUserProjects()
        {
            //var userId = repository.UserId;

            //var result = repository.All()
            //    .Where(p => p.EmployeeUserProjects.Any(eup => eup.EmployeeUserId == userId))
            //    .Select(p => new ProjectViewModel()
            //    {
            //        Name = p.Name,
            //        Status = p.Status.ToString(),
            //        StartDate = p.StartDate,
            //        EndDate = p.EndDate,
            //        Description = p.Description
            //    });

            //return result;
            var userId = repository.UserId;

            var projects = repository.All().Include(p => p.EmployeeUserProjects)
                .Where(p => p.EmployeeUserProjects.Any(eup => eup.EmployeeUserId == userId))
                .ToList();

            return mapper.Map<List<Project>, List<ProjectDto>>(projects);
        }
    }
}
