﻿using DataLayer;
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
        public void EmployeeService_All_ShouldReturn_AllActiveEmployees()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeSystemContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new EmployeeSystemContext(dbOptions);

            var userResolver = new Mock<UserResolverService>(null, null);
            var employeeUserRepository = new Mock<GenericRepository<EmployeeUser>>(db, Mock.Of<IUserResolver>());
            var userManager = new Mock<UserManager<AspUser>>(Mock.Of<IUserStore<AspUser>>(), null, null, null, null, null, null, null, null);

            var employeeService = new EmployeeService(employeeUserRepository.Object, null, null, null, null, userManager.Object);
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

            db.AddRange(firstEmployee, secondEmployee, thirdEmployee);
            db.SaveChanges();

            var result = employeeService.All();

            result.Should().HaveCount(2);
        }

        [Fact]
        public void Delete_Employee_Should_Set_NotActive()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeSystemContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new EmployeeSystemContext(dbOptions);

            var userResolver = new Mock<UserResolverService>(null, null);
            var employeeUserRepository = new Mock<GenericRepository<EmployeeUser>>(db, Mock.Of<IUserResolver>());
            var userManager = new Mock<UserManager<AspUser>>(Mock.Of<IUserStore<AspUser>>(), null, null, null, null, null, null, null, null);

            var employeeService = new EmployeeService(employeeUserRepository.Object, null, null, null, null, userManager.Object);
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
    }
}
