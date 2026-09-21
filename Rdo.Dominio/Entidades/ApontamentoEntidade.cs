using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Dominio.Entidades
{
    public class ApontamentoEntidade
    {
        public Guid Id { get; set; }
        public Guid DiarioId { get; set; }
        public Guid ServicoId { get; set; }
        public decimal QuantidadeExecutada { get; set; }


        // Relacionamentos
        public DiarioEntidade Diario { get; set; } = null!;
        public ServicoEntidade Servico { get; set; } = null!;
    }
}
