using DatLayer.Interfaces;
using DbEntities.Models;
using DTOs.Enums;
using DTOs.ViewModels;
using EmployeeSystem.Areas.AdminControlls.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ServiceLayer.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<EmployeeUser> employeeRepository;
        private readonly IRepository<Project> projectRepository;
        private readonly IRepository<EmployeeUserProject> employeeUserProjectRepository;

        public ReportService(
            IRepository<EmployeeUser> employeeRepository,
            IRepository<Project> projectRepository,
            IRepository<EmployeeUserProject> employeeUserProjectRepository)
        {
            this.employeeRepository = employeeRepository;
            this.projectRepository = projectRepository;
            this.employeeUserProjectRepository = employeeUserProjectRepository;
        }

        public ChartViewModel GetReport(int reportTypeId)
        {
            var result = new ChartViewModel();
            var data = new List<DataPoint>();

            switch (reportTypeId)
            {
                case (int)ReportType.EmployeesByPositions:
                    data = GetEmployeesByPosition();
                    break;
                case (int)ReportType.ProjectAssignedEmployees:
                    data = GetProjectAssignedEmployees();
                    break;
                default:
                    result = null;
                    break;
            }

            if (result != null)
            {
                result.Data = JsonConvert.SerializeObject(data);
                result.ChartTitle = GetReportName(reportTypeId);
            }

            return result;
        }

        private List<DataPoint> GetEmployeesByPosition()
        {
            var result = employeeRepository.All()
                .Include(e => e.EmployeePosition)
                .Where(e => e.IsActive)
                .GroupBy(e => e.EmployeePosition.Name)
                .Select(e => new DataPoint()
                {
                    Label = e.Key ?? "Not defined",
                    Y = e.Count()
                })
                .ToList();

            return result;
        }

        private List<DataPoint> GetProjectAssignedEmployees()
        {
            var result = employeeUserProjectRepository.All()
                .Include(e => e.Project)
                .Include(e => e.EmployeeUser)
                .GroupBy(p => p.Project.Name)
                .Select(e => new DataPoint()
                {
                    Label = e.Key,
                    Y = e.Select(eup => eup.EmployeeUser).Where(u => u.IsActive).Count()
                })
                .ToList();

            return result;
        }

        private string GetReportName(int type)
        {
            var name = ((ReportType)type).ToString();
            FieldInfo fi = (new ReportType()).GetType().GetField(name);
            var attribute = (DescriptionAttribute)fi.GetCustomAttribute(typeof(DescriptionAttribute));

            return attribute.Description;
        }
    }
}
