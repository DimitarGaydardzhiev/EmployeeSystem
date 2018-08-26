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
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using Xunit;

namespace EmployeeSystem.Tests.Services
{
    public class RequestServiceTest
    {
        [Fact]
        public void DeleteRequest_Should_Be_Successfulls()
        {
            var db = InitContext();
            var requestService = InitService(db);

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

        [Fact]
        public void Request_ForSameDates_ShouldThrow_Exception()
        {
            var db = InitContext();
            var requestService = InitService(db);

            var request1 = new Request()
            {
                Id = 1,
                From = DateTime.Now.Date,
                To = DateTime.Now.Date.AddDays(10)
            };

            var request2 = new Request()
            {
                Id = 2,
            };


            db.AddRange(request1, request2);
            db.SaveChanges();

            db.SaveChanges();

            var exception = Assert.Throws<Exception>(() => requestService.Save(new RequestDto()
            {
                Name = "First",
                From = DateTime.Now.Date,
                To = DateTime.Now.Date.AddDays(5)
            }));
            Assert.Equal("There is already a request for these dates", exception.Message);
        }

        [Fact]
        public void ApproveRequest_ShouldThrow_Exception_IfNoRequestFound()
        {
            var db = InitContext();
            var requestService = InitService(db);

            var request1 = new Request()
            {
                Id = 1,
                From = DateTime.Now.Date,
                To = DateTime.Now.Date.AddDays(10)
            };

            db.AddRange(request1);
            db.SaveChanges();

            var exception = Assert.Throws<Exception>(() => requestService.ApproveRequest(2));
            Assert.Equal("Request not found", exception.Message);
        }

        [Fact]
        public void DeleteRequest_ShouldThrow_If_Approved()
        {
            var db = InitContext();
            var requestService = InitService(db);

            var request = new Request()
            {
                Id = 1,
                IsApproved = true
            };

            db.AddRange(request);
            db.SaveChanges();

            var exception = Assert.Throws<InvalidDeleteException>(() => requestService.Delete(1));
            Assert.Equal(ErrorMessages.ConNotDeleteApprovedRequestMessage, exception.Message);
        }

        private EmployeeSystemContext InitContext()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeSystemContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var db = new EmployeeSystemContext(dbOptions);

            return db;
        }

        private RequestService InitService(EmployeeSystemContext db)
        {
            var userResolver = new Mock<UserResolverService>(null, null);
            var requestRepository = new Mock<GenericRepository<Request>>(db, Mock.Of<IUserResolver>());
            var employeeUserProjectRepository = new Mock<GenericRepository<EmployeeUserProject>>(db, Mock.Of<IUserResolver>());

            return new RequestService(requestRepository.Object, Mock.Of<IRequestTypeService>(), null);
        }
    }
}
