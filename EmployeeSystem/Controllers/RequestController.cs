using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System;

namespace EmployeeSystem.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class RequestController : Controller
    {
        private readonly IRequestService service;

        public RequestController(IRequestService service)
        {
            this.service = service;
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.RequestTypes = service.GetRequestTypes();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Add(RequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            service.Add(model);

            return RedirectToAction("MyRequests");
        }

        [HttpGet]
        public IActionResult MyRequests()
        {
            var result = service.GetMyRequests();
            return View(result);
        }

        [HttpGet]
        public IActionResult Pending()
        {
            var result = service.GetPendingRequests();
            return View(result);
        }

        [HttpPost]
        public IActionResult Approve(int requestId)
        {
            service.ApproveRequest(requestId);
            return RedirectToAction("Pending");
        }

        [HttpPost]
        public IActionResult Unapprove(int requestId)
        {
            service.UnapproveRequest(requestId);
            return RedirectToAction("Approved");
        }

        [HttpGet]
        public IActionResult Approved()
        {
            var result = service.GetApprovedRequests();
            return View(result);
        }
    }
}