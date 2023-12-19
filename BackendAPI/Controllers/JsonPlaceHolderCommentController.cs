using BackendAPI.Models.Entities;
using BackendAPI.Models.Repository;
using BackendAPI.Models.Repository.IRepository;
using BackendAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JsonPlaceHolderCommentController : Controller
    {
        private readonly IJsonPlaceHolder _jsonPlaceHolder;
        private readonly IJsonPlaceHolderCommentRepository _jsonPlaceHolderCommentRepositorio;

        public JsonPlaceHolderCommentController(IJsonPlaceHolder jsonPlaceHolder, IJsonPlaceHolderCommentRepository jsonPlaceHolderCommentRepositorio)
        {
            _jsonPlaceHolder = jsonPlaceHolder;
            _jsonPlaceHolderCommentRepositorio = jsonPlaceHolderCommentRepositorio;
        }

        [HttpPost, Route("FillCommentJsonPlaceHolder")]
        public async Task<ActionResult> FillPostJsonPlaceHolder()
        {
            var comment = await _jsonPlaceHolder.GetComment();

            if (comment == null)
            {
                return BadRequest("Error when obtaining the comments from JsonPlaceHolder.");
            }
            else
            {
                await _jsonPlaceHolderCommentRepositorio.SaveCommentsJson(comment);

                return StatusCode(StatusCodes.Status200OK, new { valor = comment, msg = "Comments entered correctly" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetComment()
        {
            try
            {
                return await _jsonPlaceHolderCommentRepositorio.GetComment();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveComment(Comment comment)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderCommentRepositorio.SaveComment(comment);
                if (resultado)
                {

                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }


                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditComment(Comment comment)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderCommentRepositorio.EditComment(comment);
                if (resultado)
                {

                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }


                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderCommentRepositorio.DeleteComment(id);
                if (resultado)
                {

                    return StatusCode(StatusCodes.Status200OK, new { valor = resultado, msg = "ok" });
                }


                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = resultado, msg = "error" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }

    }
}
