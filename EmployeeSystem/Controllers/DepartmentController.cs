using DTOs.Enums;
using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.ErrorUtils;
using ServiceLayer.Interfaces;
using ServiceLayer.Utils;
using System;

namespace EmployeeSystem.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService service;

        public DepartmentController(IDepartmentService service, IToastNotification toastNotification)
            : base(toastNotification)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult All()
        {
            var departments = service.All();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]
        public IActionResult Add(DepartmentViewModel model)
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

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return this.BadRequest();

            try
            {
                service.Delete(id);
                ShowNotification(SuccessMessages.SuccesslDelete, ToastrSeverity.Success);
                return RedirectToAction("All", null);
            }
            catch (Exception ex)
            {
                ShowNotification(ex.Message, ToastrSeverity.Error);
                return RedirectToAction("All", null);
            }
        }
    }
}