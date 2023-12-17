using BackendAPI.Models.Entidades;
using BackendAPI.Models.Repositorio;
using BackendAPI.Models.Repositorio.IRepositorio;
using BackendAPI.Servicios.IServicios;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JsonPlaceHolderCommentController : Controller
    {
        private readonly IJsonPlaceHolder _jsonPlaceHolder;
        private readonly IJsonPlaceHolderCommentRepositorio _jsonPlaceHolderCommentRepositorio;

        public JsonPlaceHolderCommentController(IJsonPlaceHolder jsonPlaceHolder, IJsonPlaceHolderCommentRepositorio jsonPlaceHolderCommentRepositorio)
        {
            _jsonPlaceHolder = jsonPlaceHolder;
            _jsonPlaceHolderCommentRepositorio = jsonPlaceHolderCommentRepositorio;
        }

        [HttpPost, Route("api/LlenarCommentJsonPlaceHolder")]
        public async Task<ActionResult> LlenarPostJsonPlaceHolder()
        {
            var comment = await _jsonPlaceHolder.ObtenerComment();

            if (comment == null)
            {
                return BadRequest("Error al obtener los comments de JsonPlaceHolder.");
            }
            else
            {
                await _jsonPlaceHolderCommentRepositorio.GuardarCommentsJson(comment);

                return StatusCode(StatusCodes.Status200OK, new { valor = comment, msg = "Comments ingresados correctamente" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> ObtenerComment()
        {
            try
            {
                return await _jsonPlaceHolderCommentRepositorio.ObtenerComment();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarComment(Comment comment)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderCommentRepositorio.GuardarComment(comment);
                if (resultado)
                {

                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }


                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarComment(Comment comment)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderCommentRepositorio.EditarComment(comment);
                if (resultado)
                {

                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }


                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarComment(int id)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderCommentRepositorio.EliminarComment(id);
                if (resultado)
                {

                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }


                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error interno del servidor");
            }
        }

    }
}
