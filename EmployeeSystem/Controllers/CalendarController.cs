using DTOs.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace EmployeeSystem.Controllers
{
    public class CalendarController : Controller
    {
        private readonly string[] Months = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        private readonly ICalendarService service;

        public CalendarController(ICalendarService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            CurrentMonthDto currentMonthData = service.GetCurrentMonthData();
            ViewBag.Months = Months;
            return View(currentMonthData);
        }
    }
}