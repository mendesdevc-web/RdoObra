using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.DTOs
{
    public class DiarioDto
    {
        public int UsuarioId { get; set; }
        public int NumeroSequencial { get; set; }
        public DateTime Data { get; set; }
        public string? Clima { get; set; }
        public string? EfetivoMaoObra { get; set; }
        public string? Equipamentos { get; set; }
        public string? Ocorrencias { get; set; }
        public string? Comentario { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
    }
}
