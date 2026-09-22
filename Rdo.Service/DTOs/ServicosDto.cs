using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.DTOs
{
    public class ServicosDto
    {
        public string Descricao { get; set; } = string.Empty;
        public string UnidadeMedida { get; set; } = string.Empty;
        public decimal QuantidadeOrcada { get; set; }
        public decimal? PesoOrcamento { get; set; }
    }
}
