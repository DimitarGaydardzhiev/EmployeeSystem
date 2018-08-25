using DataLayer;
using DataLayer.Interfaces;
using DatLayer;
using DbEntities.Models;
using EmployeeSystem.Data;
using EmployeeSystem.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServiceLayer.Services;
using System;
using Xunit;

namespace EmployeeSystem.Tests.Services
{
    public class EmployeeServiceTest
    {
        [Fact]
        public void Delete_Employee_Should_Set_NotActive()
        {
            var db = InitContext();
            var employeeService = InitService(db);

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
                IsActive = true
            };

            var thirdEmployee = new EmployeeUser()
            {
                Id = 3,
                FirstName = "Third Employee",
                IsActive = true
            };

            db.AddRange(firstEmployee, secondEmployee, thirdEmployee);
            db.SaveChanges();

            employeeService.Delete(1);

            db.EmployeeUsers.Should().ContainSingle(e => !e.IsActive);
        }

        private EmployeeSystemContext InitContext()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeSystemContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString())
                 .Options;

            var db = new EmployeeSystemContext(dbOptions);

            return db;
        }

        private EmployeeService InitService(EmployeeSystemContext db)
        {
            var userResolver = new Mock<UserResolverService>(null, null);
            var employeeUserRepository = new Mock<GenericRepository<EmployeeUser>>(db, Mock.Of<IUserResolver>());
            var userManager = new Mock<UserManager<AspUser>>(Mock.Of<IUserStore<AspUser>>(), null, null, null, null, null, null, null, null);

            return new EmployeeService(employeeUserRepository.Object, null, null, null, null, userManager.Object, null);
        }
    }
}
