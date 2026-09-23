using Rdo.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.DTOs.Usuarios
{
    public class UsuarioEditarDto
    {
        [EmailAddress(ErrorMessage = "Email Invalido")]
        public string Email { get; set; } = string.Empty;
        public string UsuarioNome { get; set; } = string.Empty;
        public CargoEnum Cargo { get; set; }
        public string? Senha { get; set; }
    }
}
