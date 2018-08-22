using DTOs.Enums;
using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Interfaces;
using ServiceLayer.Utils;
using System;

namespace EmployeeSystem.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService service;

        public ProjectController(IProjectService service, IToastNotification toastNotification)
            : base(toastNotification)
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
            if (ModelState.IsValid)
            {
                try
                {
                    service.Add(model);
                }
                catch (Exception e)
                {
                    ShowNotification(e.Message, ToastrSeverity.Error);
                    return View("Add", model);
                }
                ShowNotification(SuccessMessages.SuccessAdd, ToastrSeverity.Success);

                return RedirectToAction("All");
            }

            return View("Add", model);
        }

        [HttpGet]
        public IActionResult My()
        {
            var result = service.GetUserProjects();
            return View(result);
        }
    }
}