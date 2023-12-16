using BackendAPI.Models.Entidades;

namespace BackendAPI.Models.Repositorio.IRepositorio
{
    public interface IGrupoFamiliarRepositorio
    {
        List<GrupoFamiliar> ObtenerGrupoFamiliar();
        bool GuardarGrupoFamiliar(GrupoFamiliar grupoFamiliar);
        bool EditarGrupoFamiliar(GrupoFamiliar grupoFamiliar);
        bool EliminarGrupoFamiliar(string cedula);
    }
}
