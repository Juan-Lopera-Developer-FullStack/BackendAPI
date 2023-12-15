using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    public class GrupoFamiliarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
