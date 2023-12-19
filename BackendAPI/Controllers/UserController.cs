using BackendAPI.Models.Entities;
using BackendAPI.Models.Repository.IRepository;
using BackendAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogPetitionRepository _LogPetitionRepositorio;
        private readonly IBearerToken _bearerToken;

        public UserController(IUserRepository userRepository, ILogPetitionRepository LogPetitionRepositorio, IBearerToken bearerToken)
        {
            _userRepository = userRepository;
            _LogPetitionRepositorio = LogPetitionRepositorio;
            _bearerToken = bearerToken;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            try
            {
                _LogPetitionRepositorio.LogPetition("GetUser", "Get", true, "Get Users");
                return await _userRepository.GetUser();
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("GetUser", "Get", false, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveUser(User user)
        {
            try
            {
                bool resultado = await _userRepository.SaveUser(user);
                if (resultado)
                {
                    _LogPetitionRepositorio.LogPetition("SaveUser", "Post", true, "Create success user: "
                        + user.UserName);
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }

                _LogPetitionRepositorio.LogPetition("SaveUser", "Post", false, "Error running GuardarUsuario");
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("SaveUser", "Post", false, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditarUsuario(User user)
        {
            try
            {
                bool resultado = await _userRepository.EditUser(user);
                if (resultado)
                {
                    _LogPetitionRepositorio.LogPetition("EditUser", "Put", true, "Update user: "
                        + user.UserName);
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }

                _LogPetitionRepositorio.LogPetition("EditUser", "Put", false, "Error running EditUser");
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("EditUser", "Put", false, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                bool resultado = await _userRepository.DeleteUser(id);
                if (resultado)
                {
                    _LogPetitionRepositorio.LogPetition("DeleteUser", "Delete", true, "Delete user: " + id);
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }

                _LogPetitionRepositorio.LogPetition("DeleteUser", "Delete", false, "Error user does not exist");
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("DeleteUser", "Delete", false, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("api/TokenUser")]
        public async Task<IActionResult> TokenUser(string userName, string password)
        {
            try
            {
                bool resultado = await _userRepository.ValidateUser(userName, password);
                if (resultado == true)
                {
                    string Jwt = _bearerToken.GenerateTokenJwt(userName, password);
                    _LogPetitionRepositorio.LogPetition("TokenUser", "Post", true, "Token successs "
                        + userName);
                    return StatusCode(StatusCodes.Status200OK, Jwt);
                }

                _LogPetitionRepositorio.LogPetition("TokenUser", "Post", false, "Error running TokenUser");
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("TokenUser", "Post", false, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
