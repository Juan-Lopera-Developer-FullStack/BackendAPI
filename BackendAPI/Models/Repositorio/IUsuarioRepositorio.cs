﻿using BackendAPI.Models.Entidades;

namespace BackendAPI.Models.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<List<Usuario>> ObtenerUsuario();
        Task<bool> GuardarUsuario(Usuario usuario);
        Task<bool> EditarUsuario(Usuario usuario);
        Task<bool> EliminarUsuario(int id);
    }
}
