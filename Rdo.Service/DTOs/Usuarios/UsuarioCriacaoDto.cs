using Rdo.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.DTOs.Usuarios
{
    public class UsuarioCriacaoDto
    {
        [Required(ErrorMessage = "O Usuário é obrigatório.")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "O Email é obrigatório."), EmailAddress(ErrorMessage = "Email Invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A Senha é obrigatório.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "A Senha não coincidem!.")]
        public string ConfirmarSenha { get; set; }
        [Required(ErrorMessage = "O Cargo é obrigatório.")]
        public  CargoEnum Cargo { get; set; }
    }
}
