using BackendAPI.Models.Entities;

namespace BackendAPI.Services.IServices
{
    public interface IJsonPlaceHolder
    {
        Task<List<Post>> GetPost();
        Task<List<Comment>> GetComment();
    }
}
