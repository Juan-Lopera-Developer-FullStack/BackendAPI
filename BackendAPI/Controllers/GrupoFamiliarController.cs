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
        private readonly ILogPeticionRepositorio _logPeticionRepositorio;

        public GrupoFamiliarController(IGrupoFamiliarRepositorio grupoFamiliarRepositorio, ILogPeticionRepositorio logPeticionRepositorio)
        {
            _GrupoFamiliarRepositorio = grupoFamiliarRepositorio;
            _logPeticionRepositorio = logPeticionRepositorio;
        }

        
        [Authorize, HttpGet]
        public IActionResult ListaGrupoFamiliar()
        {
            try
            {
                List<GrupoFamiliar> _lista = _GrupoFamiliarRepositorio.ObtenerGrupoFamiliar();
                _logPeticionRepositorio.LogPeticion("ListaGrupoFamiliar", "Get", true, "Se obtiene la lista de grupo familiar");
                return StatusCode(StatusCodes.Status200OK, _lista);
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("ListaGrupoFamiliar", "Get", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Authorize, HttpPost]
        public IActionResult GuardarGrupoFamiliar(GrupoFamiliar grupoFamiliar)
        {
            try
            {
                bool resultado = _GrupoFamiliarRepositorio.GuardarGrupoFamiliar(grupoFamiliar);
                if (resultado)
                {
                    _logPeticionRepositorio.LogPeticion("GuardarGrupoFamiliar", "Post", true, "Se creo exitoso el familiar " 
                        + grupoFamiliar.Nombres + " con la cedula " + grupoFamiliar.Cedula);
                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }

                _logPeticionRepositorio.LogPeticion("GuardarGrupoFamiliar", "Post", false, "Error al ejecutando GuardarGrupoFamiliar");
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("GuardarGrupoFamiliar", "Post", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
            
        }

        [Authorize, HttpPut]
        public IActionResult EditarGrupoFamiliar(GrupoFamiliar grupoFamiliar)
        {
            try
            {
                bool resultado = _GrupoFamiliarRepositorio.EditarGrupoFamiliar(grupoFamiliar);
                if (resultado)
                {
                    _logPeticionRepositorio.LogPeticion("EditarGrupoFamiliar", "Put", true, "Se actulizo los datos del usuario " 
                        + grupoFamiliar.Usuario);
                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }
                _logPeticionRepositorio.LogPeticion("EditarGrupoFamiliar", "Put", false, "Error al ejecutando EditarGrupoFamiliar");
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("EditarGrupoFamiliar", "Put", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Authorize, HttpDelete]
        public IActionResult EliminarGrupoFamiliar(string cedula)
        {
            try
            {
                bool resultado = _GrupoFamiliarRepositorio.EliminarGrupoFamiliar(cedula);
                if (resultado)
                {
                    _logPeticionRepositorio.LogPeticion("EliminarGrupoFamiliar", "Delete", true, "Se elimino la cedula: " + cedula);
                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }

                _logPeticionRepositorio.LogPeticion("EliminarGrupoFamiliar", "Delete", false, "Error Cedula no existe");
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("EliminarGrupoFamiliar", "Delete", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
