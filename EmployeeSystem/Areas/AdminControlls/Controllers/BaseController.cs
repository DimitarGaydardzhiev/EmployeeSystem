using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.Areas.AdminControlls.Controllers
{
    [Area("AdminControlls")]
    [Authorize(Roles = "administrator")]
    public class BaseController : Controller
    {
    }
}