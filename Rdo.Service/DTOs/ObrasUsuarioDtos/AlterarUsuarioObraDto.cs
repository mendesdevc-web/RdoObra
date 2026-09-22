using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.DTOs.ObrasUsuarioDtos
{
    public class AlterarUsuarioObraDto
    {
        [Required]
        public string Papel { get; set; }
    }
}
