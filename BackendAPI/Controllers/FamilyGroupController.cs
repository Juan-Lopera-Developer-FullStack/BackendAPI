using BackendAPI.Models.Entities;
using BackendAPI.Models.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyGroupController : Controller
    {
        private readonly IFamilyGroupRepository _FamilyGroupRepositorio;
        private readonly ILogPetitionRepository _LogPetitionRepositorio;

        public FamilyGroupController(IFamilyGroupRepository FamilyGroupRepositorio, ILogPetitionRepository LogPetitionRepositorio)
        {
            _FamilyGroupRepositorio = FamilyGroupRepositorio;
            _LogPetitionRepositorio = LogPetitionRepositorio;
        }

        
        [Authorize, HttpGet]
        public IActionResult GetFamilyGroup()
        {
            try
            {
                List<FamilyGroup> _lista = _FamilyGroupRepositorio.GetFamilyGroup();
                _LogPetitionRepositorio.LogPetition("GetFamilyGroup", "Get", true, "The family group list is obtained");
                return StatusCode(StatusCodes.Status200OK, _lista);
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("GetFamilyGroup", "Get", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Authorize, HttpPost]
        public IActionResult SaveFamilyGroup(FamilyGroup FamilyGroup)
        {
            try
            {
                bool resultado = _FamilyGroupRepositorio.SaveFamilyGroup(FamilyGroup);
                if (resultado)
                {
                    _LogPetitionRepositorio.LogPetition("SaveFamilyGroup", "Post", true, "The family is created successful " 
                        + FamilyGroup.Nombres + " con la cedula " + FamilyGroup.Cedula);
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }

                _LogPetitionRepositorio.LogPetition("SaveFamilyGroup", "Post", false, "Error running SaveFamilyGroup");
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("SaveFamilyGroup", "Post", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Authorize, HttpPut]
        public IActionResult EditFamilyGroup(FamilyGroup FamilyGroup)
        {
            try
            {
                bool resultado = _FamilyGroupRepositorio.EditFamilyGroup(FamilyGroup);
                if (resultado)
                {
                    _LogPetitionRepositorio.LogPetition("EditFamilyGroup", "Put", true, "Update family " 
                        + FamilyGroup.Usuario);
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }
                _LogPetitionRepositorio.LogPetition("EditFamilyGroup", "Put", false, "Error running EditFamilyGroup");
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("EditFamilyGroup", "Put", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Authorize, HttpDelete]
        public IActionResult DeleteFamilyGroup(string cedula)
        {
            try
            {
                bool resultado = _FamilyGroupRepositorio.DeleteFamilyGroup(cedula);
                if (resultado)
                {
                    _LogPetitionRepositorio.LogPetition("DeleteFamilyGroup", "Delete", true, "is remove Identification: " + cedula);
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }

                _LogPetitionRepositorio.LogPetition("DeleteFamilyGroup", "Delete", false, "Error Identification does not exist");
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                _LogPetitionRepositorio.LogPetition("DeleteFamilyGroup", "Delete", false, ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
