using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System;

namespace EmployeeSystem.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService service;

        public ProjectController(IProjectService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult All()
        {
            var result = service.GetCompanyProjects();
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public IActionResult Add()
        {
            var result = service.GetEmployees();
            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public IActionResult Add(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            service.Add(model);

            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult My()
        {
            var result = service.GetUserProjects();
            return View(result);
        }
    }
}