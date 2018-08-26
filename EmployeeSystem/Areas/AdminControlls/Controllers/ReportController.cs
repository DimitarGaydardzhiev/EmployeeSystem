using DTOs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.Interfaces;

namespace EmployeeSystem.Areas.AdminControlls.Controllers
{
    [Area("AdminControlls")]
    [Authorize(Roles = "administrator")]
    public class ReportController : Controller
    {
        private readonly IReportService service;

        public ReportController(IReportService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetReport(int reportTypeId)
        {
            var dataPoints = service.GetReport(reportTypeId);
            return View("Index", dataPoints);
        }
    }
}