using AutoMapper;
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
    public class PositionServiceTests
    {
        [Fact]
        public void DeletePositiont_ShouldThrow_If_HasEmployees()
        {
            var db = InitContext();
            var positionService = InitService(db);

            var position = new EmployeePosition()
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

            db.AddRange(position);
            db.SaveChanges();

            var exception = Assert.Throws<InvalidDeleteException>(() => positionService.Delete(1));
            Assert.Equal(ErrorMessages.HasEmployeesMessage, exception.Message);
        }

        private EmployeeSystemContext InitContext()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeSystemContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var db = new EmployeeSystemContext(dbOptions);

            return db;
        }

        private PositionService InitService(EmployeeSystemContext db)
        {
            var userResolver = new Mock<UserResolverService>(null, null);
            var positionRepository = new Mock<GenericRepository<EmployeePosition>>(db, Mock.Of<IUserResolver>());
            var employeeUserRepository = new Mock<GenericRepository<EmployeeUser>>(db, Mock.Of<IUserResolver>());
            
            return new PositionService(positionRepository.Object, employeeUserRepository.Object, null);
        }
    }
}
