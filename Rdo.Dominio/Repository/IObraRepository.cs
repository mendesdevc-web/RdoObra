using Rdo.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Dominio.Repository
{
    public interface IObraRepository
    {
        Task AddAsync(UsuarioEntidade usuario);
        Task UpdateAsync(UsuarioEntidade usuario);
        Task CommitAsync();

        Task<List<ObraEntidade>> BuscarObras();
        Task<ObraEntidade?> BuscarObraId(Guid id);
        Task<List<ObraEntidade?>> BuscarObrasOrganizacao(string id);
        Task<ObraEntidade?> BuscarNomeObra(string nomeObra);
        Task<ObraEntidade> CriarObra(ObraEntidade obra);
        Task<ObraEntidade> EditarObra(Guid id, ObraEntidade obra);
    }
}
