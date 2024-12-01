using Microsoft.AspNetCore.Mvc;

namespace ElectroPromesa.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
