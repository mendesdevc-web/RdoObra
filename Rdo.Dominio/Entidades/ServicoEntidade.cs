using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rdo.Dominio.Entidades
{
    public class ServicoEntidade
    {
        public Guid Id { get; set; }
        public Guid ObraId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string UnidadeMedida { get; set; } = string.Empty;
        public decimal QuantidadeOrcada { get; set; }
        public decimal? PesoOrcamento { get; set; }

        // Relacionamentos
        [JsonIgnore]
        public ObraEntidade Obra { get; set; } = null!;

        [JsonIgnore]
        public ICollection<ApontamentoEntidade> Apontamentos { get; set; }
            = new List<ApontamentoEntidade>();
    }
}
