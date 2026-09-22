using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.DTOs
{
    public class ObraUsuarioDto
    {
        [Required]
        public Guid ObraId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string Papel { get; set; }
    }
}
