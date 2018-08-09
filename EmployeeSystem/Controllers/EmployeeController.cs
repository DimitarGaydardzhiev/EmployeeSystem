using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace EmployeeSystem.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService service;

        public EmployeeController(IEmployeeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult All()
        {
            var employees = service.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Roles = service.GetRoles();
            ViewBag.Positions = service.GetPositions();
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel model)
        {
            return null;
        }
    }
}