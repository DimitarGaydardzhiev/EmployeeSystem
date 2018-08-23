using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.Interfaces;

namespace EmployeeSystem.Areas.AdminControlls.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IReportService service;

        public HomeController(IReportService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            //List<DataPoint> dataPoints = new List<DataPoint>
            //{
            //    new DataPoint("Samsung", 25),
            //    new DataPoint("Micromax", 13),
            //    new DataPoint("Lenovo", 8),
            //    new DataPoint("Intex", 7),
            //    new DataPoint("Reliance", 6.8),
            //    new DataPoint("Others", 40.2)
            //};

            var dataPoints = service.GetEmpoyeesData();
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}