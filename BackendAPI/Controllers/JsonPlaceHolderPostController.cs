﻿
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
        private readonly IJsonPlaceHolder _jsonPlaceHolder;
        private readonly IJsonPlaceHolderPostRepositorio _jsonPlaceHolderPostRepositorio;

        public JsonPlaceHolderPostController(IJsonPlaceHolder jsonPlaceHolder, IJsonPlaceHolderPostRepositorio jsonPlaceHolderPostRepositorio)
        {
            _jsonPlaceHolder = jsonPlaceHolder;
            _jsonPlaceHolderPostRepositorio = jsonPlaceHolderPostRepositorio;
        }

        [HttpPost, Route("api/LlenarPostJsonPlaceHolder")]
        public async Task<ActionResult> LlenarPostJsonPlaceHolder()
        {
            var post = await _jsonPlaceHolder.ObtenerPost();

            if(post == null)
            {
                return BadRequest("Error al obtener los post de JsonPlaceHolder.");
            }
            else
            {
                await _jsonPlaceHolderPostRepositorio.GuardarPostsJson(post);

                return StatusCode(StatusCodes.Status200OK, new { valor = post, msg = "Posts ingresados correctamente" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> ObtenerPosts()
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
        public async Task<IActionResult> GuardarPost(Post posts)
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
        public async Task<IActionResult> EditarPosts(Post posts)
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
