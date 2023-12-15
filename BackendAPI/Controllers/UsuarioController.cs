using BackendAPI.Models.Repositorio;
using BackendAPI.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Data;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private IConfiguration _config;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IConfiguration config)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _config = config;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> MostrarUsuarios()
        {
            return await _usuarioRepositorio.ObtenerUsuario();
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [HttpPost]
        [Route("api/TokenUsuario")]
        public async Task<IActionResult> TokenUsuario(string usuario, string contraseña)
        {
            bool resultado = await _usuarioRepositorio.ValidarUsuario(usuario,contraseña);
            if (resultado == true)
            {
                string Jwt = GenerarTokenJwt(usuario,contraseña);
                return StatusCode(StatusCodes.Status200OK, new { valor = Jwt, msg = "ok" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
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
