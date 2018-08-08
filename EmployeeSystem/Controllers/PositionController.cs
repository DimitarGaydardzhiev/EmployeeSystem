using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

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
    }
}
