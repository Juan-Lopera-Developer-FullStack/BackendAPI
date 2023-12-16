using BackendAPI.Models.Entidades;
using BackendAPI.Models.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private IConfiguration _config;
        private readonly ILogPeticionRepositorio _logPeticionRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IConfiguration config, ILogPeticionRepositorio logPeticionRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _config = config;
            _logPeticionRepositorio = logPeticionRepositorio;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> MostrarUsuarios()
        {
            try
            {
                _logPeticionRepositorio.LogPeticion("MostrarUsuarios", "Get", true, "Se obtiene la lista de Usuarios");
                return await _usuarioRepositorio.ObtenerUsuario();
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("MostrarUsuarios", "Get", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GuardarUsuario(Usuario usuario)
        {
            try
            {
                bool resultado = await _usuarioRepositorio.GuardarUsuario(usuario);
                if (resultado)
                {
                    _logPeticionRepositorio.LogPeticion("GuardarUsuario", "Post", true, "Se creo exitoso el usuario "
                        + usuario.User);
                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }

                _logPeticionRepositorio.LogPeticion("GuardarUsuario", "Post", false, "Error al ejecutando GuardarUsuario");
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("GuardarUsuario", "Post", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            try
            {
                bool resultado = await _usuarioRepositorio.EditarUsuario(usuario);
                if (resultado)
                {
                    _logPeticionRepositorio.LogPeticion("EditarUsuario", "Put", true, "Se actulizo los datos del usuario "
                        + usuario.User);
                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }

                _logPeticionRepositorio.LogPeticion("EditarUsuario", "Put", false, "Error ejecutando EditarUsuario");
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("EditarUsuario", "Put", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                bool resultado = await _usuarioRepositorio.EliminarUsuario(id);
                if (resultado)
                {
                    _logPeticionRepositorio.LogPeticion("EliminarUsuario", "Delete", true, "Se elimino el usuario: " + id);
                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }

                _logPeticionRepositorio.LogPeticion("EliminarUsuario", "Delete", false, "Error usuario no existe");
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("EliminarUsuario", "Delete", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        [Route("api/TokenUsuario")]
        public async Task<IActionResult> TokenUsuario(string usuario, string contraseña)
        {
            try
            {
                bool resultado = await _usuarioRepositorio.ValidarUsuario(usuario, contraseña);
                if (resultado == true)
                {
                    string Jwt = GenerarTokenJwt(usuario, contraseña);
                    _logPeticionRepositorio.LogPeticion("TokenUsuario", "Post", true, "Se creo el token del usuario "
                        + usuario);
                    return StatusCode(StatusCodes.Status200OK, new { valor = Jwt, msg = "ok" });
                }

                _logPeticionRepositorio.LogPeticion("TokenUsuario", "Post", false, "Error al ejecutando TokenUsuario");
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _logPeticionRepositorio.LogPeticion("TokenUsuario", "Post", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        private string GenerarTokenJwt(string usuario, string contraseña)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario),
                new Claim(ClaimTypes.SerialNumber, contraseña)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
