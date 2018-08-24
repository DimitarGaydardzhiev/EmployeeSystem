using DataLayer;
using DataLayer.Interfaces;
using DatLayer;
using DbEntities.Models;
using EmployeeSystem.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using Xunit;

namespace EmployeeSystem.Tests.Services
{
    public class RequestServiceTests
    {
        [Fact]
        public void DeleteRequest_Should_Be_Successfulls()
        {
            var db = InitContext();

            var userResolver = new Mock<UserResolverService>(null, null);
            var requestRepository = new Mock<GenericRepository<Request>>(db, Mock.Of<IUserResolver>());
            var employeeUserProjectRepository = new Mock<GenericRepository<EmployeeUserProject>>(db, Mock.Of<IUserResolver>());

            var requestService = new RequestService(requestRepository.Object, Mock.Of<IRequestTypeService>());

            var request1 = new Request()
            {
                Id = 1,
                IsApproved = true
            };

            var request2 = new Request()
            {
                Id = 2,
                IsApproved = false
            };

            var request3 = new Request()
            {
                Id = 3,
                IsApproved = false
            };

            db.AddRange(request1, request2, request3);
            db.SaveChanges();

            requestService.Delete(2);

            db.Request.Should().HaveCount(2);
        }

        private EmployeeSystemContext InitContext()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeSystemContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var db = new EmployeeSystemContext(dbOptions);

            return db;
        }
    }
}
