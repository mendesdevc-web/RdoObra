using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Dominio.Entidades
{
    public class HistoricoStatusEntidade
    {
        public Guid Id { get; set; }
        public Guid DiarioId { get; set; }
        public int UsuarioId { get; set; }
        public string StatusAnterior { get; set; } = string.Empty;
        public string StatusNovo { get; set; } = string.Empty;
        public string? Comentario { get; set; }
        public DateTime DataHora { get; set; }


        // Relacionamentos
        public DiarioEntidade Diario { get; set; } = null!;
        public UsuarioEntidade Usuario { get; set; } = null!;
    }
}
