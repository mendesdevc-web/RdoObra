using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.DTOs
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage ="O campo email é obrigatório"), EmailAddress(ErrorMessage ="Email inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage ="O campo senha é obrigatorio")]
        public string Senha { get; set; }
    }
}
