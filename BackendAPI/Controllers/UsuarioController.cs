using BackendAPI.Models.Repositorio;
using BackendAPI.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> MostrarUsuarios()
        {
            return await _usuarioRepositorio.ObtenerUsuario();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarUsuario(Usuario usuario)
        {
            bool resultado = await _usuarioRepositorio.GuardarUsuario(usuario);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new {valor = resultado, msg = "ok"});
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            bool resultado = await _usuarioRepositorio.EditarUsuario(usuario);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            bool resultado = await _usuarioRepositorio.EliminarUsuario(id);
            if (resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
        }
    }
}
