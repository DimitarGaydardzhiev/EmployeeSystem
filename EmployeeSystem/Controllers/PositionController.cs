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
    public class PositionController : BaseController
    {
        private readonly IPositionService service;

        public PositionController(IPositionService service, IToastNotification toastNotification)
            : base(toastNotification)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult All()
        {
            var positions = service.All();
            return View(positions);
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public IActionResult Edit(PositionViewModel model)
        {
            return View("Add", model);
        }

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public IActionResult Save(PositionViewModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    service.Save(model);
                }
                catch (Exception e)
                {
                    ShowNotification(e.Message, ToastrSeverity.Error);
                    return RedirectToAction("Add");
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
