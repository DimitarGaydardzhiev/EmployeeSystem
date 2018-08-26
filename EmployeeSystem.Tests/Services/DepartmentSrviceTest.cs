using DataLayer;
using DataLayer.Interfaces;
using DatLayer;
using DbEntities.Models;
using DTOs.Models;
using EmployeeSystem.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServiceLayer.ErrorUtils;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace EmployeeSystem.Tests.Services
{
    public class DepartmentSrviceTest
    {
        [Fact]
        public void AddDepartment_ShouldThrow_If_Department_WithTheSmaeName_AlreadyExists()
        {
            var db = InitContext();
            var departmentService = InitService(db);

            var department = new Department()
            {
                Id = 1,
                Name = "Test"
            };

            db.AddRange(department);
            db.SaveChanges();

            var exception = Assert.Throws<Exception>(() => departmentService.Save(new DepartmentDto()
            {
                Name = "Test"
            }));

            Assert.Equal(ErrorMessages.ObjectAlreadyAddedMessage, exception.Message);
        }

        [Fact]
        public void DeleteDepartment_ShouldThrow_If_HasEmployees()
        {
            var db = InitContext();
            var departmentService = InitService(db);

            var department = new Department()
            {
                Id = 1,
                Employees = new List<EmployeeUser>()
                {
                    new EmployeeUser()
                    {
                        Id = 1
                    }
                }
            };

            db.AddRange(department);
            db.SaveChanges();

            var exception = Assert.Throws<InvalidDeleteException>(() => departmentService.Delete(1));
            Assert.Equal(ErrorMessages.HasEmployeesMessage, exception.Message);
        }

        [Fact]
        public void DeleteRequest_Should_Be_Successfulls()
        {
            var db = InitContext();
            var departmentService = InitService(db);

            var department1 = new Department()
            {
                Id = 10
            };

            var department2 = new Department()
            {
                Id = 11
            };

            var department3 = new Department()
            {
                Id = 12
            };

            db.AddRange(department1, department2, department3);
            db.SaveChanges();

            departmentService.Delete(12);

            db.Departments.Should().HaveCount(2);
        }

        private EmployeeSystemContext InitContext()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeSystemContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var db = new EmployeeSystemContext(dbOptions);

            return db;
        }

        private DepartmentService InitService(EmployeeSystemContext db)
        {
            var userResolver = new Mock<UserResolverService>(null, null);
            var departmentRepository = new Mock<GenericRepository<Department>>(db, Mock.Of<IUserResolver>());
            var employeeUserRepository = new Mock<GenericRepository<EmployeeUser>>(db, Mock.Of<IUserResolver>());

            return new DepartmentService(departmentRepository.Object, employeeUserRepository.Object, null);
        }
    }
}
