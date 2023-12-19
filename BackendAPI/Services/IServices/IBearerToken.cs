namespace BackendAPI.Services.IServices
{
    public interface IBearerToken
    {
        string GenerateTokenJwt(string user, string password);
    }
}
