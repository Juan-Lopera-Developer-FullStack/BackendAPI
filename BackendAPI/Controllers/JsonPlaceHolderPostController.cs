
using BackendAPI.Models.Entities;
using BackendAPI.Models.Repository;
using BackendAPI.Models.Repository.IRepository;
using BackendAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JsonPlaceHolderPostController : Controller
    {
        private readonly IJsonPlaceHolder _jsonPlaceHolder;
        private readonly IJsonPlaceHolderPostRepository _jsonPlaceHolderPostRepository;

        public JsonPlaceHolderPostController(IJsonPlaceHolder jsonPlaceHolder, IJsonPlaceHolderPostRepository jsonPlaceHolderPostRepositorio)
        {
            _jsonPlaceHolder = jsonPlaceHolder;
            _jsonPlaceHolderPostRepository = jsonPlaceHolderPostRepositorio;
        }

        [HttpPost, Route("FillPostJsonPlaceHolder")]
        public async Task<ActionResult> FillPostJsonPlaceHolder()
        {
            var post = await _jsonPlaceHolder.GetPost();

            if(post == null)
            {
                return BadRequest("Error when obtaining the posts from JsonPlaceHolder.");
            }
            else
            {
                await _jsonPlaceHolderPostRepository.SavePostsJson(post);

                return StatusCode(StatusCodes.Status200OK, new { value = post, msg = "Posts entered correctly" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            try
            {
                return await _jsonPlaceHolderPostRepository.GetPost();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePost(Post posts)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderPostRepository.SavePost(posts);
                if (resultado)
                {
                    
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }

                
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditPosts(Post posts)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderPostRepository.EditPost(posts);
                if (resultado)
                {
                    
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }

                
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                bool resultado = await _jsonPlaceHolderPostRepository.DeletePost(id);
                if (resultado)
                {
                    
                    return StatusCode(StatusCodes.Status200OK, new { value = resultado, msg = "ok" });
                }

                
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = resultado, msg = "error" });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
