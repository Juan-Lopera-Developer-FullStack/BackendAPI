using BackendAPI.Models.Entities;

namespace BackendAPI.Models.Repository.IRepository
{
    public interface IJsonPlaceHolderPostRepository
    {
        Task<List<Post>> GetPost();
        Task<bool> SavePostsJson(List<Post> posts);
        Task<bool> EditPost(Post posts);
        Task<bool> DeletePost(int id);
        Task<bool> SavePost(Post posts);
    }
}
