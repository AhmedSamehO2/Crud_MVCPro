using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sneat.PL.Helper;

namespace Sneat.PL.Controllers
{
    public class PermissionController : Controller
    {
       // [Authorize(Permmisions.Employee.View)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
