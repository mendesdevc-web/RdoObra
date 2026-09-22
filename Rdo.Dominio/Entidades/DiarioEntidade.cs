using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rdo.Dominio.Entidades
{
    public class DiarioEntidade
    {
        public Guid Id { get; set; }
        public Guid ObraId { get; set; }
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


        // Relacionamentos

        [JsonIgnore]
        public ObraEntidade Obra { get; set; } = null!;
        
        [JsonIgnore]
        public UsuarioEntidade Usuario { get; set; } = null!;

        [JsonIgnore]
        public ICollection<ApontamentoEntidade> Apontamentos { get; set; }
            = new List<ApontamentoEntidade>();

        [JsonIgnore]
        public ICollection<HistoricoStatusEntidade> HistoricosStatus { get; set; }
            = new List<HistoricoStatusEntidade>();
    }
}
