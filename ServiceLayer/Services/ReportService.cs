using System;
using System.Collections.Generic;
using System.Linq;
using DatLayer.Interfaces;
using DbEntities.Models;
using EmployeeSystem.Areas.AdminControlls.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<EmployeeUser> employeeRepository;

        public ReportService(IRepository<EmployeeUser> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<DataPoint> GetEmpoyeesData()
        {
            var result = employeeRepository.All()
                .Include(e => e.EmployeePosition)
                .Where(e => e.IsActive)
                .GroupBy(e => e.EmployeePosition.Name)
                .Select(e => new DataPoint()
                {
                    Label = e.Key ?? "Not defined",
                    Y = e.Count()
                });

            return result;
        }
    }
}
