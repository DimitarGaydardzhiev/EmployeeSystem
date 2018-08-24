using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace EmployeeSystem.Areas.AdminControlls.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IReportService service;

        public HomeController(IReportService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}