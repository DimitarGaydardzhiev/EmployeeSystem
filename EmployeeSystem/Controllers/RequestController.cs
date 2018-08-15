using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace EmployeeSystem.Controllers
{
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
            return View();
        }

        //[HttpPost]
        //public IActionResult Add(PositionInputModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new Exception();
        //    }

        //    service.Add(model);

        //    return RedirectToAction("All");
        //}
    }
}