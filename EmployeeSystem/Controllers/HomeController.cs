using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeSystem.Models;

namespace EmployeeSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorModelDto { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
