using BackendAPI.Models.Entidades;
using BackendAPI.Servicios.IServicios;

namespace BackendAPI.Servicios
{
    public class JsonPlaceHolder : IJsonPlaceHolder
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceHolder(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> ObtenerPost()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");

            if (response.IsSuccessStatusCode)
            {
                var postJson = await response.Content.ReadFromJsonAsync<List<Post>>();
                return postJson;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Comment>> ObtenerComment()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/comments");

            if (response.IsSuccessStatusCode)
            {
                var postJson = await response.Content.ReadFromJsonAsync<List<Comment>>();
                return postJson;
            }
            else
            {
                return null;
            }
        }
    }
}
