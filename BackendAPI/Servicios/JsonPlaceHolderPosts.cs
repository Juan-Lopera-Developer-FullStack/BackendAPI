using BackendAPI.Models.Entidades;
using BackendAPI.Servicios.IServicios;

namespace BackendAPI.Servicios
{
    public class JsonPlaceHolderPosts : IJsonPlaceHolderPosts
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceHolderPosts(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Posts>> ObtenerPost()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");

            if (response.IsSuccessStatusCode)
            {
                var postJson = await response.Content.ReadFromJsonAsync<List<Posts>>();
                return postJson;
            }
            else
            {
                return null;
            }
        }
    }
}
