
using BackendAPI.Models.Entidades;
using BackendAPI.Models.Repositorio;
using BackendAPI.Models.Repositorio.IRepositorio;
using BackendAPI.Servicios.IServicios;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JsonPlaceHolderPostController : Controller
    {
        private readonly IJsonPlaceHolderPosts _jsonPlaceHolderPosts;
        private readonly IJsonPlaceHolderPostRepositorio _jsonPlaceHolderPostRepositorio;

        public JsonPlaceHolderPostController(IJsonPlaceHolderPosts jsonPlaceHolderPosts, IJsonPlaceHolderPostRepositorio jsonPlaceHolderPostRepositorio)
        {
            _jsonPlaceHolderPosts = jsonPlaceHolderPosts;
            _jsonPlaceHolderPostRepositorio = jsonPlaceHolderPostRepositorio;
        }

        [HttpPost, Route("/LlenarPostJsonPlaceHolder")]
        public async Task<ActionResult> LlenarPostJsonPlaceHolder()
        {
            var post = await _jsonPlaceHolderPosts.ObtenerPost();

            if(post == null)
            {
                return BadRequest("Error al obtener los post de JsonPlaceHolderPosts.");
            }
            else
            {
                await _jsonPlaceHolderPostRepositorio.GuardarPostsJson(post);

                return StatusCode(StatusCodes.Status200OK, new { valor = post, msg = "Posts ingresados correctamente" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Posts>>> ObtenerPosts()
        {
            try
            {
                return await _jsonPlaceHolderPostRepositorio.ObtenerPost();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPost(Posts posts)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderPostRepositorio.GuardarPost(posts);
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
        public async Task<IActionResult> EditarPosts(Posts posts)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderPostRepositorio.EditarPost(posts);
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
        public async Task<IActionResult> EliminarPost(int id)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderPostRepositorio.EliminarPost(id);
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
