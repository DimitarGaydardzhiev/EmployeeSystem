using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace EmployeeSystem.Areas.AdminControlls.Controllers
{
    [Area("AdminControlls")]
    public class ReportController : Controller
    {
        private readonly IReportService service;

        public ReportController(IReportService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}