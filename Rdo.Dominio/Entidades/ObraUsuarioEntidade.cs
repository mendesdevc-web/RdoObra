using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Dominio.Entidades
{
    public class ObraUsuarioEntidade
    {
        public Guid Id { get; set; }
        public Guid ObraId { get; set; }
        public int UsuarioId { get; set; }
        public string Papel { get; set; } = string.Empty;


        // Relacionamentos
        public ObraEntidade Obra { get; set; } = null!;
        public UsuarioEntidade Usuario { get; set; } = null!;
    }
}
