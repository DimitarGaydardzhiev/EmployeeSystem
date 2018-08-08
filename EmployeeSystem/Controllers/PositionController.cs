using DTOs.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System;

namespace EmployeeSystem.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PositionController : Controller
    {
        private readonly IPositionService service;

        public PositionController(IPositionService service)
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PositionInputModel model)
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
