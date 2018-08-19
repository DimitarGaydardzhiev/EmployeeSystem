using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System;

namespace EmployeeSystem.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService service;

        public DepartmentController(IDepartmentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult All()
        {
            var departments = service.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            service.Add(model);

            return RedirectToAction("All");
        }
    }
}