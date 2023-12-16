using BackendAPI.Models.Entidades;
using BackendAPI.Models.Repositorio;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public IActionResult ListaGrupoFamiliar()
        {
            List<GrupoFamiliar> _lista = _GrupoFamiliarRepositorio.ObtenerGrupoFamiliar();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [Authorize]
        [HttpPost]
        public IActionResult GuardarGrupoFamiliar(GrupoFamiliar grupoFamiliar)
        {
            bool resultado = _GrupoFamiliarRepositorio.GuardarGrupoFamiliar(grupoFamiliar);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditarGrupoFamiliar(GrupoFamiliar grupoFamiliar)
        {
            bool resultado = _GrupoFamiliarRepositorio.EditarGrupoFamiliar(grupoFamiliar);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
        }

        [Authorize]
        [HttpDelete]
        public IActionResult EliminarGrupoFamiliar(string cedula)
        {
            bool resultado = _GrupoFamiliarRepositorio.EliminarGrupoFamiliar(cedula);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
        }
    }
}
