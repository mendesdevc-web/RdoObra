using Rdo.Dominio.Enum;

namespace Rdo.Dominio.Entidades
{
    public class UsuarioEntidade
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UsuarioNome { get; set; } = string.Empty;
        public CargoEnum Cargo { get; set; }
        public byte[] SenhaHash { get; set; } = [];
        public byte[] SenhaSalt { get; set; } = [];
        public DateTime TokenDataCriacao { get; set; }

        // Relacionamentos
        public ICollection<ObraUsuarioEntidade> ObrasUsuarios { get; set; }
            = new List<ObraUsuarioEntidade>();

        public ICollection<DiarioEntidade> Diarios { get; set; }
            = new List<DiarioEntidade>();

        public ICollection<HistoricoStatusEntidade> HistoricosStatus { get; set; }
            = new List<HistoricoStatusEntidade>();
    }
}
