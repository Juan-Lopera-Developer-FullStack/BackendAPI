namespace BackendAPI.Servicios.IServicios
{
    public interface IBearerToken
    {
        string GenerarTokenJwt(string usuario, string contraseña);
    }
}
