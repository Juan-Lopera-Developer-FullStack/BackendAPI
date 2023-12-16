using BackendAPI.Models.Entidades;

namespace BackendAPI.Models.Repositorio
{
    public interface IGrupoFamiliarRepositorio
    {
        List<GrupoFamiliar> ObtenerGrupoFamiliar();
        bool GuardarGrupoFamiliar(GrupoFamiliar grupoFamiliar);
        bool EditarGrupoFamiliar(GrupoFamiliar grupoFamiliar);
        bool EliminarGrupoFamiliar(string cedula);
    }
}
