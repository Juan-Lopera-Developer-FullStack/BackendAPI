﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendAPI.Models.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Usuario es requerido")]
        public string User { get; set; }
        public string Password { get; set; }
    }
}