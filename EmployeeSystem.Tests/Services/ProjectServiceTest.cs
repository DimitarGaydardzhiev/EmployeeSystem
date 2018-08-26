using DataLayer;
using DataLayer.Interfaces;
using DatLayer;
using DbEntities.Models;
using DTOs.Models;
using EmployeeSystem.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace EmployeeSystem.Tests.Services
{
    public class ProjectServiceTest
    {
        [Fact]
        public void GetCompanyProjects_ShouldReturn_AllProjects_WithActiveEmployeesCount()
        {
            var db = InitContext();
            var projectService = InitService(db);

            var employeeUserProject1 = new EmployeeUserProject()
            {
                Id = 1,
                ProjectId = 1
            };

            var employeeUserProject2 = new EmployeeUserProject()
            {
                Id = 2,
                ProjectId = 2
            };

            var firstEmployee = new EmployeeUser()
            {
                Id = 1,
                FirstName = "First Employee",
                IsActive = true
            };

            var secondEmployee = new EmployeeUser()
            {
                Id = 2,
                FirstName = "Second Employee",
                IsActive = false
            };

            var thirdEmployee = new EmployeeUser()
            {
                Id = 3,
                FirstName = "Third Employee",
                IsActive = true
            };

            var firstProject = new Project()
            {
                Id = 1,
                Name = "First Project"
            };

            var secondProject = new Project()
            {
                Id = 2,
                Name = "Second Project"
            };

            employeeUserProject1.EmployeeUser = firstEmployee;
            employeeUserProject2.EmployeeUser = secondEmployee;
            employeeUserProject1.Project = firstProject;
            employeeUserProject2.Project = secondProject;
            firstProject.EmployeeUserProjects = new List<EmployeeUserProject>() { employeeUserProject1 };
            firstEmployee.EmployeeUserProjects = new List<EmployeeUserProject>() { employeeUserProject1 };

            db.AddRange(employeeUserProject1, employeeUserProject1);

            db.AddRange(firstEmployee, secondEmployee, thirdEmployee);

            db.AddRange(firstProject, secondProject);
            db.SaveChanges();

            var result = projectService.GetCompanyProjects();

            result.Should().HaveCount(2);
        }

        [Fact]
        public void GetCompanyProjects_ShouldReturn_AllProjects()
        {
            var db = InitContext();
            var projectService = InitService(db);

            var firstProject = new Project();
            var secondProject = new Project();

            db.AddRange(firstProject, secondProject);
            db.SaveChanges();

            var result = projectService.GetCompanyProjects();

            result.Should().HaveCount(2);
        }

        [Fact]
        public void Add_Project_ShouldThrowException_IfProjectAlreadyExists()
        {
            var db = InitContext();
            var projectService = InitService(db);

            var firstProject = new Project() { Name = "First" };
            var secondProject = new Project() { Name = "Second" }; ;

            db.AddRange(firstProject, secondProject);
            db.SaveChanges();

            var exception = Assert.Throws<Exception>(() => projectService.Save(new ProjectDto()
            {
                Name = "First"
            }));
            Assert.Equal("Object already exists in the database", exception.Message);
        }

        private EmployeeSystemContext InitContext()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeSystemContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var db = new EmployeeSystemContext(dbOptions);

            return db;
        }

        private ProjectService InitService(EmployeeSystemContext db)
        {
            var userResolver = new Mock<UserResolverService>(null, null);
            var projectRepository = new Mock<GenericRepository<Project>>(db, Mock.Of<IUserResolver>());
            var employeeUserProjectRepository = new Mock<GenericRepository<EmployeeUserProject>>(db, Mock.Of<IUserResolver>());

            return new ProjectService(projectRepository.Object, Mock.Of<IEmployeeService>(), employeeUserProjectRepository.Object, null);
        }
    }
}
