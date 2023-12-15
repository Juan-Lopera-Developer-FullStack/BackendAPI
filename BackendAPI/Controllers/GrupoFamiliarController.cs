using BackendAPI.Models.Entidades;
using BackendAPI.Models.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoFamiliarController : Controller
    {
        private readonly IGrupoFamiliarRepositorio _GrupoFamiliarRepositorio;

        public GrupoFamiliarController(IGrupoFamiliarRepositorio grupoFamiliarRepositorio)
        {
            _GrupoFamiliarRepositorio = grupoFamiliarRepositorio;
        }
        [HttpGet]
        public IActionResult ListaGrupoFamiliar()
        {
            List<GrupoFamiliar> _lista = _GrupoFamiliarRepositorio.ObtenerGrupoFamiliar();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }
    }
}
