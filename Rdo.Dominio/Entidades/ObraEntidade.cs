using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Dominio.Entidades
{
    public class ObraEntidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string ResponsavelTecnico { get; set; } = string.Empty;
        public string? Status { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        // Relacionamentos
        public ICollection<ObraUsuarioEntidade> ObrasUsuarios { get; set; }
            = new List<ObraUsuarioEntidade>();

        public ICollection<ServicoEntidade> Servicos { get; set; }
            = new List<ServicoEntidade>();

        public ICollection<DiarioEntidade> Diarios { get; set; }
            = new List<DiarioEntidade>();
    }
}
