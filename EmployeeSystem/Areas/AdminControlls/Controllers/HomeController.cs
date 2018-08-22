using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.Areas.AdminControlls.Controllers
{
    [Area("AdminControlls")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}