
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

        [HttpPost]
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
    }
}
